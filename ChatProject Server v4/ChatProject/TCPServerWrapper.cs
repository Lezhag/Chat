using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using PostOffice;

namespace ChatProject
{
    public static class TcpServerWrapper
    {
        #region Variables and Declarations
        private const int ConnectionsQueue = 100;
        private static TcpListener _listener;
        private static TcpClient _socket;
        private static Parcel _serverParcel;
        public static CancellationTokenSource CancelClientConnection;

        private static Dictionary<string, ClientHandler> SocketConnections =
            new Dictionary<string, ClientHandler>();

        public static event EventHandler<Parcel> ServerParcelReceived;
        public static event EventHandler<ClientConnectedArgs> ClientConnected;
        public static event EventHandler<ClientDisconnectedArgs> ClientDisconnected;
        private static ClientHandler _cHandler;
        #endregion

        #region Initial Connection

        public static void StartServer(Connection c, Parcel firstParcel)
        {
            try
            {
                CancelClientConnection = new CancellationTokenSource();
                _serverParcel = new Parcel { UserName = firstParcel.UserName, Colour = firstParcel.Colour };
                _listener = new TcpListener(c.IP, c.Port);
                _listener.Start(ConnectionsQueue);
                MakeClientConnection(firstParcel);
            }
            catch (Exception ex)
            {
                //To do: add exception handling
                Console.WriteLine("TCPServer listener:" + ex);
            }
        }

        private static void MakeClientConnection(Parcel p)
        {
            if (CancelClientConnection.IsCancellationRequested) return;

            try
            {
                p.TimeStamp = DateTime.Now;
                _listener.BeginAcceptTcpClient(ClientConnectionCallback, p);
            }
            catch (Exception ex)
            {
                Console.WriteLine("make client connection error" + ex);
            }
        }

        private static void ClientConnectionCallback(IAsyncResult ar)
        {
            if (CancelClientConnection.IsCancellationRequested) return;
            try
            {
                _socket = new TcpClient();
                _socket = _listener.EndAcceptTcpClient(ar);
                if (_socket == null) return; //something went wrong or no connection
                _cHandler = new ClientHandler(_socket);
                SocketConnections.Add(_cHandler.Id, _cHandler);
                //subscribe to event from the client handler class
                _cHandler.ParcelArrived += cHandler_ParcelArrived;
                //Admin welcome message
                Parcel initParcel = (Parcel)ar.AsyncState;
                initParcel.TimeStamp = DateTime.Now;

                _cHandler.StartClient(_socket);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                //quietly closing the socket - no connection and no traces in the main
                _socket.Close();
            }
            MakeClientConnection((Parcel)ar.AsyncState);
        }

        #endregion

        #region Sending Messages

        public static void SendMessage(string textMessage)
        {
            _serverParcel.Msg = textMessage;
            _serverParcel.TimeStamp = DateTime.Now;
            //update DB with the server message
            TCPServerDB.AddNewMessage(_serverParcel);
            SendParcel(_serverParcel);
        }

        //method to send every connected user a message
        private static void SendParcel(Parcel parcelToSend)
        {
            foreach (var client in SocketConnections)
            {
                client.Value.SendParcel(parcelToSend);
            }
        }

        public static Parcel TextToParcel(string txt)
        {
            _serverParcel.Msg = txt;
            _serverParcel.TimeStamp = DateTime.Now;
            return _serverParcel;
        }

        #endregion

        #region Event Handlers

        private static void cHandler_ParcelArrived(object sender, Parcel e)
        {
            if (e.Msg.Contains("***has connected***"))
                //a special connection message
                OnClientConnected(new ClientConnectedArgs
                {
                    Id = e.SenderID,
                    UserName = e.UserName,
                    ConnectionTime = e.TimeStamp
                });
            if (e.Msg.Contains("***HAS LEFT THE CHAT***"))
                // a special disconnection message
                OnClientDisconnected(new ClientDisconnectedArgs
                {
                    Id = e.SenderID,
                    UserName = e.UserName,
                    ConnectionTime = e.TimeStamp
                });
            if (e.Msg.Contains("***Checking"))
            //client checks whether the username is registered
            //such messsages are not sent to other clients
            {
                //extractUserName from the message
                string extractedUserName = e.Msg.Substring("***Checking".Length,
                    e.Msg.IndexOf("***Name") - "***Checking".Length);
                //extract the name from the message
                string extractedName = e.Msg.Substring(e.Msg.IndexOf("***Name") + "***Name".Length,
                    e.Msg.Length - "***Name".Length - e.Msg.IndexOf("***Name"));

                //query DB findByUserName
                if (TCPServerDB.FindByUserName(extractedUserName) != null)
                {
                    //if UserName found, then send confirmation message to client
                    SendMessage("***Checked" + extractedUserName + "***Name");
                    return;
                }

                //sent without a name - 
                if (extractedName == string.Empty)
                {
                    //if UserName not found then send NoSuchUser
                    SendMessage("***nosuchuser");
                    return;
                }
                TCPServerDB.AddNewUser(extractedUserName, extractedName);
                //after the UserName and private name are registered, then send confirmation message to client
                SendMessage("***Checked" + extractedUserName + "***Name");
                return;
            }
            SendParcel(e);
            OnServerParcelReceived(e);
        }

        private static void OnClientDisconnected(ClientDisconnectedArgs e)
        {
            //update DB
            TCPServerDB.UpdateUserConnectionStatus(e.UserName, false);
            ClientHandler ch;
            //trying to remove the disconnected user
            SocketConnections.TryGetValue(e.Id, out ch);
            if (ch != null)
            {
                ch.TokenSourceConnection.Cancel();
                ch.Dispose();
                SocketConnections.Remove(e.Id);
            }

            EventHandler<ClientDisconnectedArgs> handler = ClientDisconnected;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        private static void OnServerParcelReceived(Parcel e)
        {
            TCPServerDB.AddNewMessage(e);
            EventHandler<Parcel> handler = ServerParcelReceived;
            if (handler != null)
            {
                handler(null, e);
            }
        }

        private static void OnClientConnected(ClientConnectedArgs firstTimeConnection)
        {
            //update DB
            TCPServerDB.UpdateUserConnectionStatus(firstTimeConnection.UserName,true);
            EventHandler<ClientConnectedArgs> handler = ClientConnected;
            if (handler != null)
            {
                handler(null, firstTimeConnection);
            }
        }

        #endregion

        #region Dispose

        public static void Close()
        {
            _listener.Stop();
            if (SocketConnections.Count <= 0) return;

            foreach (var entry in SocketConnections)
                entry.Value.Dispose();
            _socket.Close();

            ClientConnected = null;
            ClientDisconnected = null;
            ServerParcelReceived = null;
        }

        #endregion
    }
}
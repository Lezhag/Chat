using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using PostOffice;

namespace ChatProject
{
    public class TcpClientWrapper
    {
        #region Variables and Declarations

        private readonly TcpClient _socket = new TcpClient();
        public CancellationTokenSource TokenSourceCheckingParcels;
        public event EventHandler<Parcel> ParcelArrived;
        public event EventHandler UserAuthorized;
        public event EventHandler UserNotAuthorized;
        private readonly BinaryFormatter _formatter = new BinaryFormatter();
        public bool Authorized = false;
        public bool NewUser = false;
        private Parcel _baseParcel;
        public bool SocketConnected
        {
            get { return _socket.Connected; }
        }

        #endregion

        #region Constructor

        public TcpClientWrapper(Connection c, Parcel firstMsg)
        {
            _baseParcel = new Parcel(firstMsg.UserName, firstMsg.TimeStamp, firstMsg.Msg, firstMsg.Colour);
            StartClient(c, firstMsg);
        }

        #endregion

        #region Client Startup

        private void StartClient(Connection c, Parcel firstMsg)
        {
            try
            {
                _socket.BeginConnect(c.IP, c.Port, new AsyncCallback(ConnectCallback), firstMsg);
            }
            catch (Exception e)
            {
                Debug.WriteLine("beginning to connect: " + e);
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            SendParcel((Parcel)ar.AsyncState);
            TokenSourceCheckingParcels = new CancellationTokenSource();
            Task readParcels = new Task(CheckForParcels, TokenSourceCheckingParcels.Token);
            readParcels.Start();
        }

        #endregion

        #region Receiving Messages

        private void CheckForParcels()
        {
            while (!TokenSourceCheckingParcels.IsCancellationRequested)
            {
                try
                {
                    if (_socket.Connected)
                    {
                        NetworkStream networkStream = _socket.GetStream();
                        if (networkStream.DataAvailable)
                        {
                            Parcel p = (Parcel) _formatter.Deserialize(networkStream);
                            if (Authorized)
                            {
                                OnParcelArrived(p);
                            }
                            else
                            {
                                if (p.Msg.Contains("***Checked"))
                                {
                                    Authorized = true;
                                    if (NewUser)
                                    {
                                        OnNewUser(EventArgs.Empty);
                                    }
                                    ExtractUserName(p);
                                }
                                if (p.Msg.Contains("***nosuchuser"))
                                {
                                    if (NewUser)
                                    {
                                        OnUserNotAuthorized(EventArgs.Empty);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        TokenSourceCheckingParcels.Cancel();
                    }
                    //await Task.Delay(500);

                }
                catch (Exception e)
                {
                    TokenSourceCheckingParcels.Cancel();
                    Console.WriteLine(e);
                }
            }
        }

        private void ExtractUserName(Parcel parcel)
        {
            _baseParcel.UserName = parcel.Msg.Substring("***Checked".Length,
                parcel.Msg.IndexOf("***Name") - "***Checked".Length);
        }

        #endregion
        
        #region Event Handlers

        private void OnUserNotAuthorized(EventArgs e)
        {
            EventHandler handler = UserNotAuthorized;
            if (handler == null) return;
            handler(this, e);
        }

        private void OnNewUser(EventArgs e)
        {
            EventHandler handler = UserAuthorized;
            if (handler == null) return;
            handler(this, e);
        }

        private void OnParcelArrived(Parcel arrivedParcel)
        {
            EventHandler<Parcel> handler = ParcelArrived;
            if (handler == null) return;
            handler(this, arrivedParcel);
        }

        #endregion

        #region Sending Messages

        private void SendParcel(Parcel parcelToSend)
        {
            if (!_socket.Connected) return;
            NetworkStream stream = _socket.GetStream();
            _formatter.Serialize(stream, parcelToSend);
            stream.Flush();
            Debug.WriteLine("tcp client sending " + parcelToSend);
        }

        public void SendParcel(string txtToSend)
        {
            _baseParcel.TimeStamp = DateTime.Now;
            _baseParcel.Msg = txtToSend;
            SendParcel(_baseParcel);
        }

        #endregion
        
        #region Dispose

        public void Dispose()
        {
            ParcelArrived = null;
            _socket.Close();
        }

        #endregion

    }
}
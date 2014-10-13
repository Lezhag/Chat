using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Threading.Tasks;
using PostOffice;

namespace ChatProject
{
    // helper class for handling client communication by the server
    public class ClientHandler : IDisposable
    {
        #region Definitions and Declarations

        //*****public fields******

        //cancellation token for initiating connection
        public CancellationTokenSource TokenSourceConnection;
        //event for Server UI notifying on <Parcel> arrival
        public event EventHandler<Parcel> ParcelArrived;
        //GUID string for unique identification of client handler
        public string Id { get; private set; }

        //*****private fields*****
        private readonly TcpClient _socket;
        private NetworkStream _networkStream;
        private BinaryFormatter _formatter = new BinaryFormatter();

        #endregion

        #region Constructors

        public ClientHandler(TcpClient s)
        {
            _socket = s;
            Id = Guid.NewGuid().ToString();
        }
        #endregion

        #region Data Manipulation


        public void StartClient(TcpClient connectionSocket)
        {
            _networkStream = connectionSocket.GetStream();
            StartReadData();
        }

        //method that starts the Task CheckForParcels 
        private void StartReadData()
        {
            TokenSourceConnection = new CancellationTokenSource();
            Task readParcels = new Task(CheckForParcels, TokenSourceConnection.Token);
            readParcels.Start();
        }

        //method for the task above - loops until TokenSourceConnection requested
        private async void CheckForParcels()
        {
            while (!TokenSourceConnection.IsCancellationRequested)
            {
                try
                {
                    if (!_socket.Connected) { TokenSourceConnection.Cancel(); continue; }
                    NetworkStream networkStream = _socket.GetStream();
                    if (!networkStream.DataAvailable)
                    {
                        //nothing to do here
                    }
                    else
                    {
                        Parcel p = (Parcel) _formatter.Deserialize(networkStream);
                        OnParcelArrived(p);
                    }
                    //pausing the task
                    await Task.Delay(500);

                }
                catch (Exception e)
                {
                    //most likely SocketException
                    TokenSourceConnection.Cancel();
                    Debug.WriteLine(e);
                }
            }
        }

        private void OnParcelArrived(Parcel arrivedParcel)
        {
            //notify the server who sent the message
            arrivedParcel.SenderID = Id;
            EventHandler<Parcel> handler = ParcelArrived;
            if (handler != null)
            { handler(this, arrivedParcel); }
        }

        public void SendParcel(Parcel parcelToSend)
        {
            _formatter.Serialize(_networkStream, parcelToSend);
            _networkStream.Flush();
            //diagnostic message
            Debug.WriteLine("Server sends: "+parcelToSend);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            ParcelArrived = null;
            _formatter = null;
            _networkStream.Close();
            _socket.Close();
        }

        #endregion

    }
}

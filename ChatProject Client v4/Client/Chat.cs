using System;
using System.Threading;
using System.Windows.Forms;
using ChatProject;
using PostOffice;

namespace Client
{
    public partial class Chat : Form
    {
        #region Variable Declarations

        private readonly TcpClientWrapper _client = null;
        private delegate void AddParcelCallback(Parcel p);

        #endregion

        public Chat(Connection c, Parcel firstMsg)
        {
            InitializeComponent();
            try
            {
                //_tokenReceivingParcels = new CancellationTokenSource();
                //_clientParcel = new Parcel(firstMsg.UserName, firstMsg.TimeStamp, "", firstMsg.Colour);
                _client = new TcpClientWrapper(c, firstMsg);
                _client.ParcelArrived += _client_ParcelArrived;
            }
            catch (Exception)
            {
                Console.WriteLine("Client: Socket Exception"); ;
            }
        }

        public Chat(TcpClientWrapper client)
        {
            _client = client;
            InitializeComponent();
        }

        #region Handling Parcel Arrived event

        private void _client_ParcelArrived(object sender, Parcel e)
        {
            if (txtChat.InvokeRequired)
            {
                AddParcelCallback del = new AddParcelCallback(_client_ParcelArrived);
                Invoke(del, new object[] { e });
            }
            else
            {
                txtChat.AppendText(e);
            }
        }

        private void _client_ParcelArrived(Parcel p)
        {
            txtChat.AppendText(p);
        }

        #endregion

        #region Receiving parcels

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMsg.Text == "") return;
            _client.SendParcel(txtMsg.Text);
            txtMsg.Text = "";
        }

        #endregion

        #region Mopping up

        private void Chat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_client == null || !_client.SocketConnected) return;
            _client.SendParcel("***HAS LEFT THE CHAT***");
            _client.TokenSourceCheckingParcels.Cancel();
            _client.Dispose();
        }
        #endregion  

        private void Chat_Load(object sender, EventArgs e)
        {
            _client.ParcelArrived += _client_ParcelArrived;
            _client.SendParcel("***has connected***");
        }
    }
}

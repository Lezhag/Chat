using System;
using System.Threading;
using System.Windows.Forms;
using ChatProject;
using PostOffice;

namespace Server
{
    public partial class ServerControl : Form
    {
        #region Variable Declarations
        // This ServerControl shows two independent ways of updating UI with content: 
        // via SyncronizationContext and via Invoke method with a callback delegate

        //class for holding UI context
        private readonly SynchronizationContext _ui;
        //delegate for UI calls
        private delegate void AddParcelCallback(Parcel p);

        #endregion

        #region Ctor

        public ServerControl(Connection c, Parcel firstMsg)
        {
            InitializeComponent();

            #region Adding ListView

            lstClients.Columns.Add("Name", -2, HorizontalAlignment.Left);
            lstClients.Columns.Add("Connection Status", -2, HorizontalAlignment.Center);
            lstClients.Columns.Add("Connection Start", -2, HorizontalAlignment.Right);
            tabMain.SelectedTab = tabChat;

            #endregion

            //event subscriptions
            TcpServerWrapper.ServerParcelReceived += TCPServer_ServerParcelReceived;
            TcpServerWrapper.ClientConnected += TCPServer_ClientConnected;
            TcpServerWrapper.ClientDisconnected += TCPServer_ClientDisconnected;

            // this variable holds the Current UI context for updating data in UI
            _ui = SynchronizationContext.Current;
            //Start of the whole programme
            TCPServerDB.Initialize();
            TcpServerWrapper.StartServer(c, firstMsg);
        }

        #endregion

        #region Sending Messages

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (txtMessage.Text != "")
            {
                tabMain.SelectedTab = tabChat;
                rtbChat.AppendText(TcpServerWrapper.TextToParcel(txtMessage.Text));
                TcpServerWrapper.SendMessage(txtMessage.Text);
                txtMessage.Text = "";
            }
        }

        #endregion

        #region Handling Events and Updating UI

        #region Parcel Received event

        //update ui with Invoke required
        private void TCPServer_ServerParcelReceived(object sender, Parcel e)
        {
            if (rtbChat.InvokeRequired)
            {
                AddParcelCallback del = new AddParcelCallback(TCPServer_ServerParcelReceived);
                Invoke(del, new object[] {e});
            }
            else
            {
                rtbChat.AppendText(e);
            }
        }

        //invoked event handler method
        private void TCPServer_ServerParcelReceived(Parcel p)
        {
            rtbChat.AppendText(p);
        }

        #endregion

        #region Client Connected event
        
        //update UI using the UI context
        private void TCPServer_ClientConnected(object sender, ClientConnectedArgs e)
        {
            _ui.Post(UpdateClientConnected, e);
        }

        private void UpdateClientConnected(object state)
        {
            ClientConnectedArgs args = state as ClientConnectedArgs;
            //if some clients are already connected
            if (lstClients.Items.Count > 0)
            {
                //let's see if the connected client is a returning customer
                foreach (ListViewItem clientRecord in lstClients.Items)
                {
                    if (clientRecord.Text == args.UserName)
                    {
                        //update connection status
                        clientRecord.SubItems[1].Text = "Connected";
                    }
                    else
                    {
                        //a client is completely new - add a new Client record
                        ListViewItem newClientRecord = new ListViewItem {Text = args.UserName};
                        newClientRecord.SubItems.Add("Connected");
                        newClientRecord.SubItems.Add(args.ConnectionTime.ToShortTimeString());
                        //newClientRecord.SubItems.Add(args.Id);
                        lstClients.Items.Add(newClientRecord);
                        tabMain.SelectedTab = tabClients;
                    }
                }
            }
            else
            {
                //First client - create a ListViewItem
                ListViewItem newClientRecord = new ListViewItem {Text = args.UserName};
                newClientRecord.SubItems.Add("Connected");
                newClientRecord.SubItems.Add(args.ConnectionTime.ToShortTimeString());
                //newClientRecord.SubItems.Add(args.Id);
                lstClients.Items.Add(newClientRecord);
                tabMain.SelectedTab = tabClients;
            }
        }

        #endregion

        #region Client Disconnected event

        private void TCPServer_ClientDisconnected(object sender, ClientDisconnectedArgs e)
        {
            _ui.Post(UpdateClientDisconnected, e);
        }

        private void UpdateClientDisconnected(object state)
        {
            ClientDisconnectedArgs args = state as ClientDisconnectedArgs;
            //we know that there's a client by that name
            foreach (ListViewItem clientRecord in lstClients.Items)
            {
                //find him an update his connection status
                if (args != null && clientRecord.Text == args.UserName)
                {
                    clientRecord.SubItems[1].Text = "Disconnected";
                }
            }
            //move to the clients tab
            tabMain.SelectedTab = tabClients;
        }

        #endregion

        #region Search Messages by Keyword or Date

        private void btnSearchKeyword_Click(object sender, EventArgs e)
        {
            if (txtKeyword.Text == string.Empty) return;
            dgvMessages.DataSource = TCPServerDB.LookUpStoredMessages(txtKeyword.Text);
        }

        private void btnSearchDate_Click(object sender, EventArgs e)
        {
            dateTimePicker1.Select();
            dgvMessages.DataSource = TCPServerDB.LookUpStoredMessages(dateTimePicker1.Value);
        }

        #endregion

        #region Search/Remove Users by ID/User name

        private void btnSearchID_Click(object sender, EventArgs e)
        {
            if (txtSearchID.Text == string.Empty) return;
            dgvUsers.DataSource = TCPServerDB.FindByIdList(txtSearchID.Text);
        }

        private void btnRemoveID_Click(object sender, EventArgs e)
        {
            if (txtSearchID.Text == string.Empty) return;
            TCPServerDB.RemoveUserbyID(txtSearchID.Text);
        }

        private void btnSearchUserName_Click(object sender, EventArgs e)
        {
            if (txtSearchUserName.Text == string.Empty) return;
            dgvUsers.DataSource = TCPServerDB.FindByUserNameList(txtSearchUserName.Text);
        }

        private void btnRemoveUserName_Click(object sender, EventArgs e)
        {
            if (txtSearchUserName.Text == string.Empty) return;
            TCPServerDB.RemoveUserbyUserName(txtSearchUserName.Text);
        }

        #endregion
        #endregion

        #region Mopping up on closing
        private void ServerControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            TcpServerWrapper.ServerParcelReceived -= TCPServer_ServerParcelReceived;
            TcpServerWrapper.ClientConnected -= TCPServer_ClientConnected;
            TcpServerWrapper.ClientDisconnected -= TCPServer_ClientDisconnected;
            TcpServerWrapper.CancelClientConnection.Cancel();
            TcpServerWrapper.Close();
        } 
        #endregion
    }
}

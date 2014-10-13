using System;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ChatProject;
using PostOffice;

namespace Client
{
    public partial class FrmStartClient : Form
    {
        #region Variable Declarations

        private delegate void NewUserCallback(EventArgs e);
        private delegate void ResetNewUserCallback(EventArgs e);

        private Chat newChat;
        private TcpClientWrapper socketClient = null;

        //private CancellationTokenSource _cancelServerCheckAvailability;
        private Connection c = new Connection();

        #endregion

        #region Ctor

        public FrmStartClient()
        {
            InitializeComponent();
            LoadColours();
            LoadDefaults();
        }

        #endregion

        #region Loading data and defaults

        private void LoadDefaults()
        {
            txtIP.Text = c.IP.ToString();
            txtPort.Text = c.Port.ToString();
            //txtName.Text = "Enter your user name here!";
            txtUserName.Text = "Client";
            cmbColours.Text = "Choose your colour here!";
            //cmbColours.Text = "Chocolate";
        }

        private void LoadColours()
        {
            foreach (PropertyInfo prop in typeof(Color).GetProperties())
            {
                if (prop.PropertyType.FullName == "System.Drawing.Color")
                    cmbColours.Items.Add(prop.Name);
            }
        }

        #endregion

        #region Click methods

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            txtUserName.Multiline = false;
            txtUserName.WordWrap = false;
            this.txtIP_Validating(sender, new CancelEventArgs());
            this.txtName_Validating(sender, new CancelEventArgs());
            this.txtPort_Validating(sender, new CancelEventArgs());
            this.cmbColours_Validating(sender, new CancelEventArgs());
            if (ValidationInput.ValidatedIP && ValidationInput.ValidatedPort && ValidationInput.ValidatedUserName &&
                ValidationInput.ValidatedColor)
            {
                c.IP = IPAddress.Parse(txtIP.Text);
                c.Port = ushort.Parse(txtPort.Text);

                CheckUserNameAvailability();
            }
        }

        private void CheckUserNameAvailability()
        {
            //if tcpclient not connected - connect and send the username
            if (socketClient == null)
            {
                socketClient = new TcpClientWrapper(c, new Parcel(txtUserName.Text, DateTime.Now, "***Checking" + txtUserName.Text + "***Name",
                                                         Color.FromName(cmbColours.SelectedItem.ToString())));
                socketClient.UserAuthorized += socketClient_UserAuthorized;
                socketClient.UserNotAuthorized += socketClient_UserNotAuthorized;

            }
            //otherwise - send a new user name and name for registration
            else
            {
                socketClient.SendParcel("***Checking" + txtUserName.Text + "***Name" + txtFullName.Text);
            }
            socketClient.NewUser = true;
        }

        private void socketClient_UserNotAuthorized(object sender, EventArgs e)
        {
            if (txtUserName.InvokeRequired && txtFullName.InvokeRequired)
            {
                ResetNewUserCallback del = new ResetNewUserCallback(socketClient_UserNotAuthorized);
                Invoke(del, new object[] { e });
            }
            else
            {
                ResetUserName();
            }
        }

        private void socketClient_UserNotAuthorized(EventArgs e)
        {
            ResetUserName();
        }

        private void ResetUserName()
        {
            //txtUserName.Height = 40;
            //txtUserName.WordWrap = true;
            //txtUserName.Text = "User Name taken: Enter a new one";
            txtFullName.Visible = true;
            lblName.Visible = true;
            txtFullName.Text = "Enter your name here";
        }

        void socketClient_UserAuthorized(object sender, EventArgs e)
        {
            if (txtUserName.InvokeRequired && txtFullName.InvokeRequired)
            {
                NewUserCallback del = new NewUserCallback(socketClient_UserAuthorized);
                Invoke(del, new object[] { e });
            }
            else
            {
                CloseAddNewUser();
            }
        }

        private void socketClient_UserAuthorized(EventArgs e)
        {
            CloseAddNewUser();
        }

        private void CloseAddNewUser()
        {
            socketClient.UserAuthorized -= socketClient_UserAuthorized;
            socketClient.UserNotAuthorized -= socketClient_UserNotAuthorized;
            socketClient.NewUser = false;
            //btnConnect.Width = 100;
            btnConnect.Text = "Connected! =>";
            btnConnect.Enabled = false;
            btnChat.Enabled = true;
        }

        private void FrmStartClient_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (socketClient != null)
            {
                socketClient.TokenSourceCheckingParcels.Cancel();
                socketClient.Dispose();
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            Hide();
            newChat = new Chat(socketClient);
            newChat.Show();
        }        
        
        #endregion

        #region Validation
        private void txtIP_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!ValidationInput.ValidateIP(txtIP.Text, out error))
            {
                errorProvider1.SetError(txtIP, error);
                e.Cancel = true;
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!ValidationInput.ValidateUserName(txtUserName.Text, out error))
            {
                errorProvider1.SetError(txtUserName, error);
                e.Cancel = true;
            }
        }

        private void txtPort_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!ValidationInput.ValidatePort(txtPort.Text, out error))
            {
                errorProvider1.SetError(txtPort, error);
                e.Cancel = true;
            }
        }

        private void cmbColours_Validating(object sender, CancelEventArgs e)
        {
            string error;
            if (!ValidationInput.ValidateColor(cmbColours.Text, out error))
            {
                errorProvider1.SetError(cmbColours, error);
                e.Cancel = true;
            }
        }

        private void cmbColours_Validated(object sender, EventArgs e)
        {
            ValidationInput.ValidatedColor = true;
        }

        private void txtIP_Validated(object sender, EventArgs e)
        {
            ValidationInput.ValidatedIP = true;
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            ValidationInput.ValidatedUserName = true;
        }

        private void txtPort_Validated(object sender, EventArgs e)
        {
            ValidationInput.ValidatedPort = true;
        }
        #endregion

    }
}

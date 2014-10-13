using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using ChatProject;
using PostOffice;


namespace Server
{
    public partial class FrmServerStart : Form
    {
        #region Parameters

        private readonly Connection _c = new Connection();

        #endregion
        
        #region Loading Defaults and Controls

        private void LoadDefaults()
        {
            txtIP.Text = _c.IP.ToString();
            txtPort.Text = _c.Port.ToString();
            txtName.Text = "Admin";
            //cmbColours.Text = "Tomato";
            cmbColours.Text = "Choose your colour here!";
        }

        private void LoadColours()
        {
            foreach (
                PropertyInfo prop in
                    typeof (Color).GetProperties().Where(prop => prop.PropertyType.FullName == "System.Drawing.Color"))
            {
                cmbColours.Items.Add(prop.Name);
            }
        }

        #endregion

        #region Ctor

        public FrmServerStart()
        {
            InitializeComponent();
            LoadColours();
            LoadDefaults();
        }

        #endregion

        #region Click Methods

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            txtIP_Validating(sender, new CancelEventArgs());
            txtName_Validating(sender, new CancelEventArgs());
            txtPort_Validating(sender, new CancelEventArgs());
            cmbColours_Validating(sender, new CancelEventArgs());

            if (ValidationInput.ValidatedIP && ValidationInput.ValidatedPort)
            {
                if (ValidationInput.ValidatedUserName && ValidationInput.ValidatedColor)
                {
                    _c.IP = IPAddress.Parse(txtIP.Text);
                    _c.Port = ushort.Parse(txtPort.Text);

                    Hide();
                    ServerControl newControl =
                        new ServerControl(_c,
                                          new Parcel(txtName.Text, DateTime.Now, txtName.Text + " welcomes you to Chat!",
                                                     Color.FromName(cmbColours.SelectedItem.ToString())));
                    newControl.Show();
                }
            }
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
            if (!ValidationInput.ValidateUserName(txtName.Text, out error))
            {
                errorProvider1.SetError(txtName, error);
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

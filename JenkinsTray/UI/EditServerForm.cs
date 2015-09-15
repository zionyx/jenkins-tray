using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors;
using Jenkins.Tray.Entities;

namespace Jenkins.Tray.UI
{
    public partial class EditServerForm : DevExpress.XtraEditors.XtraForm
    {
        public EditServerForm()
        {
            InitializeComponent();
            urlTextBox.TextChanged += delegate { ValidateForm(); };
        }

        public EditServerForm(Server server)
            : this()
        {
            ServerAddress = server.Url;
            ServerName = server.DisplayName;
            IgnoreUntrustedCertificate = server.IgnoreUntrustedCertificate;
            if (server.Credentials != null)
            {
                RequiresAuthentication = true;
                Username = server.Credentials.Username;
                Password = server.Credentials.Password;
            }
        }

        public string ServerAddress
        {
            get { return urlTextBox.Text; }
            set { urlTextBox.Text = value; }
        }

        public string ServerName
        {
            get { return displayNameTextEdit.Text; }
            set { displayNameTextEdit.Text = value; }
        }

        public bool RequiresAuthentication
        {
            get { return authenticationCheckBox.CheckState == CheckState.Checked; }
            set { authenticationCheckBox.CheckState = value ? CheckState.Checked : CheckState.Unchecked; }
        }

        public string Username
        {
            get
            {
                if (RequiresAuthentication == false)
                    return null;
                return usernameTextBox.Text;
            }
            set { usernameTextBox.Text = value; }
        }

        public string Password
        {
            get
            {
                if (RequiresAuthentication == false)
                    return null;
                return passwordTextBox.Text;
            }
            set { passwordTextBox.Text = value; }
        }

        public bool IgnoreUntrustedCertificate
        {
            get { return ignoreUntrustedCertificateCheckBox.CheckState == CheckState.Checked; }
            set { ignoreUntrustedCertificateCheckBox.CheckState = value ? CheckState.Checked : CheckState.Unchecked; }
        }

        protected virtual bool IsUrlValid(string url)
        {
            url = url.Trim();
            bool valid = String.IsNullOrEmpty(url) == false && url != "http://" && url != "https://";
            return valid;
        }

        private void ValidateForm()
        {
            string url = urlTextBox.Text;
            bool nameValid = IsUrlValid(url);

            validateButton.Enabled = nameValid;
        }

        private void authenticationCheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            usernameLabel.Enabled
                = usernameTextBox.Enabled
                = passwordLabel.Enabled
                = passwordTextBox.Enabled
                = RequiresAuthentication;
        }

        private void urlTextBox_Leave(object sender, EventArgs e)
        {
            string url = ServerAddress;
            if (String.IsNullOrEmpty(url) == false
                && url.StartsWith("http://") == false
                && url.StartsWith("https://") == false)
            {
                ServerAddress = "http://" + url;
            }
        }

        private void EditServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            urlTextBox_Leave(null, null);
        }
    }
}
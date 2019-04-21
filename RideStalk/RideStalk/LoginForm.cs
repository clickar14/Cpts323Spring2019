using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RideStalk
{
    public partial class LoginForm : MetroFramework.Forms.MetroForm
    {
        // initialize login panel
        public LoginForm()
        {
            InitializeComponent();
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        // login button on login panel
        private void loginButton_Click(object sender, EventArgs e)
        {
            if (usernameField.Text == "admin" && passwordField.Text == "password")
            {
                // close the current window and move to the interface
                this.Close();
            }
            else
            {
                // make register panel visible if login is invalid
                registerPanal.Visible = true;
            }
        }

        // register button on register panel
        private void registerButton_Click(object sender, EventArgs e)
        {
            // check if something was entered into the textboxes
            if ((nameEntry.Text.Length > 0) &&
                (passwordEntry.Text.Length > 0) &&
                (emailEntry.Text.Length > 0) &&
                (PhoneEntry.Text.Length > 0) &&
                (addressEntry.Text.Length > 0) &&
                (urlEntry.Text.Length > 0))
            {
                // close the current window and move to the interface
                this.Close();
            }
        }

        // button to go back to login panel on register panel
        private void back_login_button_Click(object sender, EventArgs e)
        {
            // go back to login panel
            registerPanal.Visible = false;
        }
    }
}

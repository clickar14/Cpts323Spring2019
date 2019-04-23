﻿using System;
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
        // valid denotes the validity of the current login
        private bool valid;
        // initialize login panel
        public LoginForm()
        {
            // initially no valid login
            valid = false;
            InitializeComponent();
            // hide registration panel
            registerPanal.Visible = false;
            this.StyleManager = metroStyleManager1;
        }

        public bool is_valid()
        {
            return valid;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        // login button on login panel
        private void loginButton_Click_1(object sender, EventArgs e)
        {
            if (usernameField.Text == "admin" && passwordField.Text == "password")
            {
                // treated as valid login
                valid = true;
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
        private void registerButton_Click_1(object sender, EventArgs e)
        {
            // check if something was entered into the textboxes
            if ((nameEntry.Text.Length > 0) &&
                (passwordEntry.Text.Length > 0) &&
                (emailEntry.Text.Length > 0) &&
                (PhoneEntry.Text.Length > 0) &&
                (addressEntry.Text.Length > 0) &&
                (urlEntry.Text.Length > 0))
            {
                // treated as valid login
                valid = true;
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

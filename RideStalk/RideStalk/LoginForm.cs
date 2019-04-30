using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Firebase.Database;
using Newtonsoft.Json;
using System.Reactive.Linq;
using Firebase.Database.Query;

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

        // returns the value of valid
        public bool is_valid()
        {
            return valid;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        // login button on login panel
        private async void loginButton_Click_1(object sender, EventArgs e)
        {
            // connect to the Firebase database
            var client = new FirebaseClient("https://test-a6194.firebaseio.com/");

            var logins = await client
              .Child(usernameField.Text)
              .OrderByKey()
              .OnceAsync<Data>();
            foreach (var login in logins)
            {
                if (usernameField.Text == login.Object.name && passwordField.Text == login.Object.password)
                {
                    // valid login
                    valid = true;
                    // close the current window and move to the interface
                    this.Close();
                }
            }
            // make register panel visible if login is invalid
            registerPanal.Visible = true;
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

                // connect to the Firebase database
                var client = new FirebaseClient("https://test-a6194.firebaseio.com/");  

                // create new child with the username entered    
                var child = client.Child(nameEntry.Text);

                // post new login on Firebase database
                child.PostAsync(new OutboundMessage {
                    name = nameEntry.Text,
                    password = passwordEntry.Text,
                    email = emailEntry.Text,
                    phone = PhoneEntry.Text,
                    address = addressEntry.Text,
                    url = urlEntry.Text
                });
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

    // timestamp for Firebase database
    public class ServerTimeStamp
    {
        [JsonProperty(".sv")]
        public string TimestampPlaceholder { get; } = "timestamp";
    }

    // store login data
    public class Data
    {
        public string name { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string url { get; set; }
    }

    // message for posting to Firebase database
    public class OutboundMessage : Data
    {
        [JsonProperty("Timestamp")]
        public ServerTimeStamp TimestampPlaceholder { get; } = new ServerTimeStamp();
    }

}

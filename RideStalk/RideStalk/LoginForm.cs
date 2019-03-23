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
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (registerPanal.Visible == false)
            {
                registerPanal.Visible = true;

            }
        }
        private void registerButton_Click(object sender, EventArgs e)
        {
            if (registerPanal.Visible == true)
            {
                registerPanal.Visible = false;

            }
        }
    }
}

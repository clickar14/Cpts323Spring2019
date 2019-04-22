using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.Database;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RideStalk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm login = new LoginForm();
            Application.Run(login);
            if (login.is_valid()) // only goes to interface if there is a valid login
                Application.Run(new Interface());
        }
    }
}

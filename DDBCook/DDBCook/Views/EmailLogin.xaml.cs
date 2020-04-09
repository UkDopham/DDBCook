using DDBCook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DDBCook.Views
{
    /// <summary>
    /// Logique d'interaction pour EmailLogin.xaml
    /// </summary>
    public partial class EmailLogin : UserControl
    {
        private RegisterOrLogin _registerOrLogin;
        public EmailLogin(RegisterOrLogin registerOrLogin)
        {
            InitializeComponent();
            this._registerOrLogin = registerOrLogin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            //check if email exists
            bool isExisting = false;
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Client> clients = ddb.SelectClient(new string[] { "email" }, new string[] { $"'{EmailTextBox.Text}'" });

            if (clients != null)
            {
                if (clients.Count != 0)
                {
                    isExisting = true;
                }
            }

            if (isExisting)
            {
                this._registerOrLogin.DataContext = new Login(EmailTextBox.Text);
            }
            else
            {
                this._registerOrLogin.DataContext = new Register(EmailTextBox.Text);
            }
        }
    }
}

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
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private string _email;
        public Login(string email)
        {
            this._email = email;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Check if password is right

            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            if (clientPasswordBox.Password == ddb.SelectClient(new string[] { "email" }, new string[] { $"'{this._email}'" })[0].Password)
            {
                User.ConnectedClient = ddb.SelectClient(new string[] { "email" }, new string[] { $"'{this._email}'" })[0];

                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                if (User.ConnectedClient.UserType == Models.Enums.UserType.admin)
                {
                    mainWindow.DataContext = new Admin();
                }
                else
                {
                    mainWindow.DataContext = new MainMenu();
                }
            }
        }
    }
}

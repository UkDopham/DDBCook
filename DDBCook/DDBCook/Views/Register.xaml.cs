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
    /// Logique d'interaction pour Register.xaml
    /// </summary>
    public partial class Register : UserControl
    {
        private string _email;
        public Register(string email)
        {
            this._email = email;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(numberTB.Text))
            {
                DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
                Client client = new Client(this._email, passwordTB.Password, Models.Enums.UserType.user, nameTB.Text, numberTB.Text, adressTB.Text);
                User.ConnectedClient = client;
                ddb.Insert<Client>(client);
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow.DataContext = new MainMenu();
            }
        }
    }
}

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
    /// Logique d'interaction pour MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {
        private Client _connectedClient;
        public MainMenu(Client client = null)
        {
            InitializeComponent();
            DataContext = new WelcomePage();
            if (client != null)
            {
                this._connectedClient = client;
                ClientTextBlock.Text = this._connectedClient.Name;
                //ClientButton.IsEnabled = false; if we want to disconnect from the client account
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new RegisterOrLogin();
        }

        private void BasketButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new BasketInformation();
        }
    }
}

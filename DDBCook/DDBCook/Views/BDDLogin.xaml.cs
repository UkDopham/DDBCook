using DDBCook.Models;
using DDBCook.Models.Gestion;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour BDDLogin.xaml
    /// </summary>
    public partial class BDDLogin : UserControl
    {
        public BDDLogin()
        {
            InitializeComponent();
            LoadTextBlock();
        }

        private void LoadTextBlock()
        {
            databaseTB.Text = "Database";
            usernameTB.Text = "Username";
            passwordTB.Text = "Password";
        }

        private void passwordTB_GotFocus(object sender, RoutedEventArgs e)
        {
            passwordTB.Text = string.Empty;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User.DataBase = databaseTB.Text;
            User.Username = usernameTB.Text;
            User.Password = passwordTB.Text;
            File.WriteAllLines(User.Path, new string[] { User.DataBase, User.Username, User.Password });

            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            ddb.Command($"create database {User.DataBase}");
            string[] create = File.ReadAllLines("creation.sql");

            foreach(string line in create) //Excute the sql script
            {
                ddb.Command(line); //creation of the table
            }
            string[] fill = File.ReadAllLines("peuplement.sql");

            foreach(string line in fill)
            {
                ddb.Command(line); //fill the table
            }
            ddb.SelectRecipe().ForEach(x => Stock.UpdateMinMaxQuantities(x));   //a rajouter a moment des commandes
            ddb.SelectRecipe().ForEach(x => Stock.ManageOrder(x));
            Stock.RottenProducts();

            ddb.Close();
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }

        private void databaseTB_GotFocus(object sender, RoutedEventArgs e)
        {
            databaseTB.Text = string.Empty;
        }

        private void usernameTB_GotFocus(object sender, RoutedEventArgs e)
        {
            usernameTB.Text = string.Empty;
        }
    }
}

using DDBCook.Models;
using DDBCook.Models.Gestion;
using DDBCook.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace DDBCook
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            ddb.SelectRecipe().ForEach(x=>Stock.UpdateMinMaxQuantities(x));   //a rajouter a moment des commandes
            ddb.SelectRecipe().ForEach(x => Stock.ManageOrder(x));
            Stock.RottenProducts();

            ddb.Close();
            InitializeComponent();




            
            DataContext = new MainMenu();
        }

        
    }
}

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

namespace DDBCook.Views.Demo
{
    /// <summary>
    /// Logique d'interaction pour DemoRecipe.xaml
    /// </summary>
    public partial class DemoRecipe : UserControl
    {
        public DemoRecipe()
        {
            InitializeComponent();
            View();
        }
        private void View()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            answerTextBlock.Text = ddb.SelectRecipe().Count.ToString();
            ddb.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new DemoProduct();
        }
    }
}

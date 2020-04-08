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
            //DataContext = new WelcomePage();
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            DataContext = new RecipesViewer(ddb.SelectRecipe());
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


        /// <summary>
        /// Sort the list of recipes depending on the name of the 'sender'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sort_Changed(object sender, RoutedEventArgs e)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Recipe> listeRecettes = ddb.SelectRecipe();
            ddb.Close();

            if (((CheckBox)sender).Name.Equals(AlphabetiqueCB.Name))
            {
                listeRecettes.Sort((a, b) => (a.Name.CompareTo(b.Name)));
                NoteCB.IsChecked = false;
                PrixCB.IsChecked = false;
            }
            if (((CheckBox)sender).Name.Equals(NoteCB.Name))
            {
                listeRecettes.Sort((a, b) => (a.Rating.CompareTo(b.Rating)));
                AlphabetiqueCB.IsChecked = false;
                PrixCB.IsChecked = false;
            }
            if (((CheckBox)sender).Name.Equals(PrixCB.Name))
            {
                listeRecettes.Sort((a, b) => (a.Price.CompareTo(b.Price)));
                NoteCB.IsChecked = false;
                AlphabetiqueCB.IsChecked = false;
            }

            DataContext = new RecipesViewer(listeRecettes);
        }

        /// <summary>
        /// Filter the list of recipes by looking at which checkboxes are checked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Filter_CheckedChanged(object sender, RoutedEventArgs e)
        {
            List<Recipe> recettesFiltrees;
            List<string> colonnesAfiltrer = new List<string>();
            List<string> valeurscolonnes = new List<string>();

            if (HealthyCB?.IsChecked == true)
            {
                colonnesAfiltrer.Add("estHealthy");
                valeurscolonnes.Add("true");
            }
            if (BioCB?.IsChecked == true)
            {
                colonnesAfiltrer.Add("estBio");
                valeurscolonnes.Add("true");
            }
            if (VeganCB?.IsChecked == true)
            {
                colonnesAfiltrer.Add("estVegan");
                valeurscolonnes.Add("true");
            }
            if (ChimiqueCB?.IsChecked == true)
            {
                colonnesAfiltrer.Add("estChimique");
                valeurscolonnes.Add("true");
            }
            if (TendanceCB?.IsChecked == true)
            {
                colonnesAfiltrer.Add("estTendance");
                valeurscolonnes.Add("true");
            }

            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            recettesFiltrees = ddb.SelectRecipe(colonnesAfiltrer.ToArray(), valeurscolonnes.ToArray());
            ddb.Close();

            DataContext = new RecipesViewer(recettesFiltrees);
            // recettesFiltrees
        }

        private void BasketButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new BasketInformation();
        }

        private void CDRButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new ClientInformation(new Client("John", "Alex", Models.Enums.UserType.user, "John doe", "1222", "adress", 123));
        }
    }
}
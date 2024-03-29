﻿using DDBCook.Models;
using DDBCook.Views.Demo;
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
    /// Logique d'interaction pour MainMenu.xaml
    /// </summary>
    public partial class MainMenu : UserControl
    {

        public MainMenu()
        {
            InitializeComponent();
            LoadSettings();
            CDRButton.Visibility = User.ConnectedClient != null ? Visibility.Visible : Visibility.Hidden;
            BaksetButton.Visibility = User.ConnectedClient != null ? Visibility.Visible : Visibility.Hidden;
            //DataContext = new WelcomePage();
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            if (ddb.IsOpen)
            {
                DataContext = new RecipesViewer(ddb.SelectRecipe());
                if (User.ConnectedClient != null)
                {
                    ClientTextBlock.Text = User.ConnectedClient.Name;
                    //ClientButton.IsEnabled = false; if we want to disconnect from the client account
                }
            }
            else
            {
                DemoButton.Visibility = Visibility.Hidden;
                UserGrid.Visibility = Visibility.Hidden;
                FilterBorder.Visibility = Visibility.Hidden;
                DataContext = new BDDLogin();
            }
        }
        /// <summary>
        /// load the saved settins ( database id ...)
        /// </summary>
        private void LoadSettings()
        {
            if(!File.Exists(User.Path)) //check if the file exists
            {
                File.WriteAllLines(User.Path, new string[] { "cook", "root", "root" });
            }
            string[] settings = File.ReadAllLines(User.Path);
            User.DataBase = settings[0];
            User.Username = settings[1];
            User.Password = settings[2];
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
            mainWindow.DataContext = new ClientInformation();
        }

        private void DemoButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new Views.Demo.DemoClient();
        }
    }
}
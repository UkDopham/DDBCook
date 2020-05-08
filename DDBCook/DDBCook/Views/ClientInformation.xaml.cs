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
    /// Logique d'interaction pour ClientInformation.xaml
    /// </summary>
    public partial class ClientInformation : UserControl
    {
        private Client _client;

        public Client Client
        {
            get
            {
                return this._client;
            }
            set
            {
                this._client = value;
            }
        }
        public string GetName
        {
            get
            {
                DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
                
                return this._client.Name +" - "+ (ddb.SelectRecipeCreator(new string[] { "numero" }, new string[] { $"'{this._client.PhoneNumber}'" }).Count > 0 ? "CDR" : "Client");
            }
        }
        public string Balance
        {
            get
            {
                return $"{this._client.Money} cook";
            }
        }
        public ClientInformation()
        {
            this._client = User.ConnectedClient;
            InitializeComponent();
            InitilizationRecipes();
            DDB ddb  = new DDB(User.DataBase, User.Username, User.Password);
            cdrGrid.Visibility = ddb.SelectRecipeCreator(new string[] { "numero" }, new string[] { $"'{this._client.PhoneNumber}'" }).Count > 0 ? Visibility.Visible : Visibility.Hidden;
            ddb.Close();
        }
        private Grid GetImageText(string text, string imageName, int size, bool center = false)
        {
            Grid grid = new Grid()
            {
                Margin = new Thickness(2)
            };

            Uri uri = new Uri($"pack://siteoforigin:,,,/Resources/{imageName}");
            BitmapImage bitmap = new BitmapImage(uri);
            Image image = new Image()
            {
                Source = bitmap,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            TextBlock textBlock = GetTextBlock(text, new SolidColorBrush(Colors.White), FontWeights.Bold, size);
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            textBlock.HorizontalAlignment = center ? HorizontalAlignment.Center : HorizontalAlignment.Left;
            grid.Children.Add(image);
            grid.Children.Add(textBlock);

            return grid;
        }
        private void InitilizationRecipes()
        {
            RecipeStackPanel.Children.Clear();
            //Get recipes created by the client
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);

            List<Recipe> recipes = ddb.SelectRecipe(new string[] { "numeroCreateur" }, new string[] { $"'{this._client.PhoneNumber}'" });
            ddb.Close();
            foreach (Recipe recipe in recipes)
            {
                AddRecipe(recipe);
            }
        }
        private void AddRecipe(Recipe recipe)
        {
            RecipeStackPanel.Children.Add(GetButton(recipe));
        }
        private ContainerButton<Recipe> GetButton(Recipe recipe)
        {
            ContainerButton<Recipe> button = new ContainerButton<Recipe>()
            {
                Margin = new Thickness(10),
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0),
                Content = GetGrid(recipe),
                Value = recipe,
            };
            button.Click += (s, e) =>
            {
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow.DataContext = new RecipeInformation(button.Value);
            };
            return button;
        }
        private Grid GetGrid(Recipe recipe)
        {
            Grid grid = new Grid()
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = 80,
                Width = 400,
                Background = new SolidColorBrush(Color.FromRgb(248, 248, 248)),
                RowDefinitions =
                {
                    new RowDefinition()
                    {
                        Height = new GridLength(3, GridUnitType.Star)
                    },
                    new RowDefinition()
                    {
                        Height = new GridLength(2, GridUnitType.Star)
                    },
                },
            };
            Grid titlegrid = new Grid()
            {
                Width = 400,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent),
                ColumnDefinitions =
                {
                    new ColumnDefinition()
                    {
                        Width = new GridLength(16, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(4, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    //new ColumnDefinition()
                    //{
                    //    Width = new GridLength(1, GridUnitType.Star)
                    //},
                    //new ColumnDefinition()
                    //{
                    //    Width = new GridLength(1, GridUnitType.Star)
                    //},
                    //new ColumnDefinition()
                    //{
                    //    Width = new GridLength(1, GridUnitType.Star)
                    //},
                    //new ColumnDefinition()
                    //{
                    //    Width = new GridLength(1, GridUnitType.Star)
                    //},
                    //new ColumnDefinition()
                    //{
                    //    Width = new GridLength(1, GridUnitType.Star)
                    //},
                },
            };
            Grid price = ImageText($"{recipe.Price} cooks", "label.png", 12);
            TextBlock name = GetTextBlock($"{StringLimit(recipe.Name)} - {recipe.Rating}", new SolidColorBrush(Colors.Black), FontWeights.Bold, 20);

            titlegrid.Children.Add(price);
            titlegrid.Children.Add(name);

            Grid.SetColumn(price, 2);
            Grid.SetColumn(name, 0);

            //Grid healthy = new Grid()
            //{
            //    Width = 10,
            //    Height = 10,
            //    Background = new SolidColorBrush(Color.FromRgb(255, 179, 186))
            //};
            //if (recipe.IsHealthy)
            //{
            //    titlegrid.Children.Add(healthy);
            //    Grid.SetColumn(healthy, 4);
            //}
            //Grid bio = new Grid()
            //{
            //    Width = 10,
            //    Height = 10,
            //    Background = new SolidColorBrush(Color.FromRgb(186, 255, 201))
            //};
            //if (recipe.IsBio)
            //{
            //    titlegrid.Children.Add(bio);
            //    Grid.SetColumn(bio, 5);
            //}
            //Grid vegan = new Grid()
            //{
            //    Width = 10,
            //    Height = 10,
            //    Background = new SolidColorBrush(Color.FromRgb(186, 225, 255))
            //};
            //if (recipe.IsVegan)
            //{
            //    titlegrid.Children.Add(vegan);
            //    Grid.SetColumn(vegan, 6);
            //}
            //Grid chimical = new Grid()
            //{
            //    Width = 10,
            //    Height = 10,
            //    Background = new SolidColorBrush(Color.FromRgb(255, 255, 186))
            //};
            //if (recipe.IsChimical)
            //{
            //    titlegrid.Children.Add(chimical);
            //    Grid.SetColumn(chimical, 7);
            //}
            //Grid trending = new Grid()
            //{
            //    Width = 10,
            //    Height = 10,
            //    Background = new SolidColorBrush(Color.FromRgb(255, 223, 186))
            //};
            //if (recipe.IsTrending)
            //{
            //    titlegrid.Children.Add(trending);
            //    Grid.SetColumn(trending, 8);
            //}


            TextBlock desc = GetTextBlock(recipe.Description, new SolidColorBrush(Colors.Black), FontWeights.Bold, 14);
            grid.Children.Add(titlegrid);
            grid.Children.Add(desc);

            Grid.SetRow(titlegrid, 0);
            Grid.SetRow(desc, 1);
            return grid;
        }
        private string StringLimit(string text, int limit = 20)
        {
            if (text.Length > limit)
            {
                string tmp = string.Empty;
                for (int i = 0;
                    i < limit;
                    i++)
                {
                    tmp += text[i];
                }
                text = $"{tmp} ...";
            }
            return text;
        }
        private Grid ImageText(string text, string imageName, int size)
        {
            Grid grid = new Grid()
            {
                Margin = new Thickness(2)
            };

            Uri uri = new Uri($"pack://siteoforigin:,,,/Resources/{imageName}");
            BitmapImage bitmap = new BitmapImage(uri);
            Image image = new Image()
            {
                Source = bitmap,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            TextBlock textBlock = GetTextBlock(text, new SolidColorBrush(Colors.White), FontWeights.Bold, size);
            textBlock.VerticalAlignment = VerticalAlignment.Center;
            grid.Children.Add(image);
            grid.Children.Add(textBlock);

            return grid;
        }
        private TextBlock GetTextBlock(string text, SolidColorBrush colorBrush, FontWeight fontWeight, int fontSize = 12)
        {
            int limit = 50;
            if (text.Length > limit)
            {
                string tmp = string.Empty;
                for (int i = 0;
                    i < limit;
                    i++)
                {
                    tmp += text[i];
                }
                text = $"{tmp} ...";
            }
            return new TextBlock()
            {
                Margin = new Thickness(5),
                Text = text,
                FontSize = fontSize,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontWeight = fontWeight,
                Foreground = colorBrush,
            };
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new RecipeCreation();
        }
    }
}

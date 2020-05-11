using DDBCook.Models;
using DDBCook.Models.Gestion;
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
    /// Logique d'interaction pour BasketInformation.xaml
    /// </summary>
    public partial class BasketInformation : UserControl
    {
        private int _count = 0;
        public BasketInformation()
        {
            InitializeComponent();
            InitializationBasket();
        }
        private void UpdateBasket(int price)
        {
            titleTB.Text = $"Panier : {price} cooks";
        }
        /// <summary>
        /// Initilization of the visual
        /// </summary>
        private void InitializationBasket()
        {
            this._count = 0;
            UpdateBasket(this._count);
            BasketStackPanel.Children.Clear();
            foreach(Recipe recipe in Basket.Recipes)
            {
                AddRow(recipe);
                this._count += recipe.Price;
                UpdateBasket(this._count);
            }
        }
        /// <summary>
        /// Add a row 
        /// </summary>
        /// <param name="recipe"></param>
        private void AddRow(Recipe recipe)
        {
            BasketStackPanel.Children.Add(GetGrid(recipe));
        }

        private Grid GetGrid(Recipe recipe)
        {
            Grid grid = new Grid()
            {
                Margin = new Thickness(10),
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Color.FromRgb(248,248,248)),
                Height = 80,
                Width = 500,
                RowDefinitions =
                {
                    new RowDefinition()
                    {
                        Height = new GridLength(2, GridUnitType.Star)
                    },
                    new RowDefinition()
                    {
                        Height = new GridLength(3, GridUnitType.Star)
                    },
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition()
                    {
                        Width = new GridLength(5, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(2, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(2, GridUnitType.Star)
                    }, 
                    new ColumnDefinition()
                    {
                        Width = new GridLength(2, GridUnitType.Star)
                    }
                }
            };            

            ContainerButton<Recipe> addButton = new ContainerButton<Recipe>()
            {
                BorderThickness = new Thickness(0),
                Background = new SolidColorBrush(Colors.Transparent),
                Content = GetImage("ajouter.png"),
                Value = recipe,
            };
            addButton.Click += (s, e) =>
            {
                //add recipe
                Basket.Recipes.Add(addButton.Value);
                //reset visual
                InitializationBasket();
            };

            ContainerButton<Recipe> deletButton = new ContainerButton<Recipe>()
            {
                BorderThickness = new Thickness(0),
                Background = new SolidColorBrush(Colors.Transparent),
                Content = GetImage("annuler.png"),
                Value = recipe,
            };
            deletButton.Click += (s, e) =>
            {
                Basket.Recipes.Remove(deletButton.Value);
                //delet recipe
                //reset visual
                InitializationBasket();
            };

            TextBlock name = GetTextBlock(recipe.Name, new SolidColorBrush(Colors.Black), FontWeights.Bold, 23);
            Grid price = GetImageText($"{recipe.Price} cook", "label.png", 14, true);

            grid.Children.Add(addButton);
            grid.Children.Add(deletButton);
            grid.Children.Add(name);
            grid.Children.Add(price);

            Grid.SetColumn(addButton, 4);
            Grid.SetColumn(deletButton, 3);
            Grid.SetColumn(name, 0);
            Grid.SetColumn(price, 1);
            Grid.SetRow(name, 1);
            Grid.SetRow(price, 1);

            return grid;
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
        private Border GetBorder(ContainerButton<Recipe> uIElement, SolidColorBrush backColorBrush)
        {
            return new Border()
            {
                Margin = new Thickness(5),
                Background = backColorBrush,
                BorderThickness = new Thickness(0),
                Child = uIElement,
            };
        }
        private Grid GetImage(string imageName)
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

            grid.Children.Add(image);

            return grid;
        }
        private TextBlock GetTextBlock(string text, SolidColorBrush colorBrush, FontWeight fontWeight, int fontSize = 12)
        {
            if (text.Length > 10)
            {
                string tmp = string.Empty;
                for (int i = 0;
                    i < 10;
                    i++)
                {
                    tmp += text[i];
                }
                text = $"{tmp} ...";
            }
            return new TextBlock()
            {
                Text = text,
                FontSize = fontSize,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = fontWeight,
                Foreground = colorBrush,
                FontFamily = titleTB.FontFamily,
            };
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            Basket.Recipes.Clear();
            this._count = 0;
            UpdateBasket(this._count);
            BasketStackPanel.Children.Clear();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            //Do command
            int count = 0;
            Basket.Recipes.ForEach(x => count += x.Price);
            if (count <= User.ConnectedClient.Money) // if the user has enough money 
            {
                if (Stock.IsPossible(Basket.Recipes))
                {
                    foreach (Recipe recipe in Basket.Recipes)
                    {
                        Order order = new Order(Guid.NewGuid().ToString(), DateTime.Now, User.ConnectedClient.PhoneNumber, recipe.Name);
                        DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
                        ddb.Insert<Order>(order);
                        Stock.ManageOrder(recipe, true);
                    }
                    DDB ddb1 = new DDB(User.DataBase, User.Username, User.Password);
                    Basket.Recipes.Clear();
                    User.ConnectedClient.Money -= count;
                    MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                    mainWindow.DataContext = new MainMenu();
                    ddb1.UpdateClient(User.ConnectedClient, new string[] { "nom" }, new string[] { $"'{User.ConnectedClient.Name}'" });
                    PopUp popUp = new PopUp("Commande passé", "Vous allez être livrer bientôt");
                    popUp.ShowDialog();
                }
                else
                {
                    //not enough stock
                    PopUp popUp = new PopUp("Panier incorrect", "Les produits ne sont pas disponibles.");
                    popUp.ShowDialog();
                }
            }
            else
            {
                //not enough money 
                PopUp popUp = new PopUp("Achat impossible", "Vous n'avez pas assez d'argent sur votre compte.");
                popUp.ShowDialog();
            }
        }
    }
}

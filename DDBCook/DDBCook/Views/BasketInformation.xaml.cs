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
    /// Logique d'interaction pour BasketInformation.xaml
    /// </summary>
    public partial class BasketInformation : UserControl
    {
        public BasketInformation()
        {
            InitializeComponent();
            InitializationBasket();
        }
        /// <summary>
        /// Initilization of the visual
        /// </summary>
        private void InitializationBasket()
        {
            BasketStackPanel.Children.Clear();
            foreach(Recipe recipe in Basket.Recipes)
            {
                AddRow(recipe);
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
                        Width = new GridLength(3, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(2, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(3, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    }, 
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    }
                }
            };            

            ContainerButton<Recipe> addButton = new ContainerButton<Recipe>()
            {
                BorderThickness = new Thickness(0),
                Background = new SolidColorBrush(Color.FromRgb(90, 193, 142)),
                Content = "+",
                Foreground = new SolidColorBrush(Colors.White),
                FontSize = 16,
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
                Background = new SolidColorBrush(Color.FromRgb(245, 0, 0)),
                Content = "-",
                FontSize = 16,
                Foreground = new SolidColorBrush(Colors.White),
                Value = recipe,
            };
            deletButton.Click += (s, e) =>
            {
                Basket.Recipes.Remove(deletButton.Value);
                //delet recipe
                //reset visual
                InitializationBasket();
            };

            Border addBorder = GetBorder(addButton, new SolidColorBrush(Color.FromRgb(90, 193, 142)));
            Border deletBorder = GetBorder(deletButton, new SolidColorBrush(Color.FromRgb(245, 0, 0)));

            TextBlock name = GetTextBlock(recipe.Name, new SolidColorBrush(Color.FromRgb(112, 111, 211)), FontWeights.Bold, 23);
            TextBlock price = GetTextBlock($"{recipe.Price} cook", new SolidColorBrush(Colors.Black), FontWeights.Bold, 14);

            grid.Children.Add(addBorder);
            grid.Children.Add(deletBorder);
            grid.Children.Add(name);
            grid.Children.Add(price);

            Grid.SetColumn(addBorder, 4);
            Grid.SetColumn(deletBorder, 3);
            Grid.SetColumn(name, 0);
            Grid.SetColumn(price, 1);
            Grid.SetRow(name, 1);
            Grid.SetRow(price, 1);

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
            BasketStackPanel.Children.Clear();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            //Do command
            int count = 0;
            Basket.Recipes.ForEach(x => count += x.Price);
            if (count <= User.ConnectedClient.Money) // if the user has enough money 
            {
                foreach (Recipe recipe in Basket.Recipes)
                {
                    Order order = new Order(Guid.NewGuid().ToString(), DateTime.Now, User.ConnectedClient.PhoneNumber, recipe.Name);
                    DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
                    ddb.Insert<Order>(order);

                }

                Basket.Recipes.Clear();
                User.ConnectedClient.Money -= count;
                MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
                mainWindow.DataContext = new MainMenu();
            }
            else
            {
                //not enough money 
            }
        }
    }
}

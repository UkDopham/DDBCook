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
    /// Logique d'interaction pour RecipesViewer.xaml
    /// </summary>
    public partial class RecipesViewer : UserControl
    {
        private List<Recipe> _recipes;
        public RecipesViewer(List<Recipe> recipes)
        {
            InitializeComponent();
            this._recipes = recipes;
            InitRecipes();
        }

        public void InitRecipes()
        {
            foreach (Recipe recipe in this._recipes)
            {
                AddRecipe(recipe);
            }
        }

        private void AddRecipe(Recipe recipe)
        {
            ContentStackPanel.Children.Add(GetButton(recipe));
        }
        private ContainerButton<Recipe> GetButton(Recipe recipe)
        {
            ContainerButton<Recipe> button = new ContainerButton<Recipe>()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                BorderThickness = new Thickness(0),
                Content = GetGrid(recipe),
                Height = 80,
                Width = 400,
                Margin = new Thickness(10),
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
                        Width = new GridLength(12, GridUnitType.Star)
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
                        Width = new GridLength(2, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                    new ColumnDefinition()
                    {
                        Width = new GridLength(1, GridUnitType.Star)
                    },
                },
            };
            TextBlock price = GetTextBlock($"{recipe.Price} cook", new SolidColorBrush(Color.FromRgb(255, 115, 115)), FontWeights.Bold, 14);
            TextBlock name = GetTextBlock(recipe.Name, new SolidColorBrush(Color.FromRgb(112, 111, 211)), FontWeights.Bold, 16);

            titlegrid.Children.Add(price);
            titlegrid.Children.Add(name);

            Grid.SetColumn(price, 2);
            Grid.SetColumn(name, 0);

            Grid healthy = new Grid()
            {
                Width = 10,
                Height = 10,
                Background = new SolidColorBrush(Color.FromRgb(255, 179, 186))
            };
            if (recipe.IsHealthy)
            {
                titlegrid.Children.Add(healthy);
                Grid.SetColumn(healthy, 4);
            }
            Grid bio = new Grid()
            {
                Width = 10,
                Height = 10,
                Background = new SolidColorBrush(Color.FromRgb(186, 255, 201))
            };
            if (recipe.IsBio)
            {
                titlegrid.Children.Add(bio);
                Grid.SetColumn(bio, 5);
            }
            Grid vegan = new Grid()
            {
                Width = 10,
                Height = 10,
                Background = new SolidColorBrush(Color.FromRgb(186, 225, 255))
            };
            if (recipe.IsVegan)
            {
                titlegrid.Children.Add(vegan);
                Grid.SetColumn(vegan, 6);
            }
            Grid chimical = new Grid()
            {
                Width = 10,
                Height = 10,
                Background = new SolidColorBrush(Color.FromRgb(255, 255, 186))
            };
            if (recipe.IsChimical)
            {
                titlegrid.Children.Add(chimical);
                Grid.SetColumn(chimical, 7);
            }
            Grid trending = new Grid()
            {
                Width = 10,
                Height = 10,
                Background = new SolidColorBrush(Color.FromRgb(255, 223, 186))
            };
            if (recipe.IsTrending)
            {
                titlegrid.Children.Add(trending);
                Grid.SetColumn(trending, 8);
            }


            TextBlock desc = GetTextBlock(recipe.Description, new SolidColorBrush(Color.FromRgb(218, 218, 218)), FontWeights.Bold, 14);
            grid.Children.Add(titlegrid);
            grid.Children.Add(desc);

            Grid.SetRow(titlegrid, 0);
            Grid.SetRow(desc, 1);
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
    }
}

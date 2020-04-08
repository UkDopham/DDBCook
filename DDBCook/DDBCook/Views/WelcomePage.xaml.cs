using DDBCook.Models;
using DDBCook.Models.Enums;
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
    /// Logique d'interaction pour WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        public WelcomePage()
        {
            InitializeComponent();
            AddRecipe(RecipeRow.cheaper, new Recipe(" CouscousCouscousCouscous", RecipeType.boisson, "test desc", "122", 23));
        }

        /// <summary>
        /// Add the recipe in the define row
        /// </summary>
        /// <param name="recipeRow"></param>
        /// <param name="recipe"></param>
        private void AddRecipe(RecipeRow recipeRow, Recipe recipe)
        {
            switch(recipeRow)
            {
                case RecipeRow.cheaper:
                    CheaperRecipeStackPanel.Children.Add(GetButton(recipe));
                    break;

                case RecipeRow.rated:
                    BestRatedStackPanel.Children.Add(GetButton(recipe));
                    break;

                case RecipeRow.trending:
                    TrendingStackPanel.Children.Add(GetButton(recipe));
                    break;
            }
        }
        /// <summary>
        /// return a custom button which contain the recipe
        /// </summary>
        /// <param name="recipe"></param>
        /// <returns></returns>
        private ContainerButton<Recipe> GetButton(Recipe recipe)
        {
            ContainerButton<Recipe> button = new ContainerButton<Recipe>()
            {
                Height = 60,
                Margin = new Thickness(5, 0, 5, 0),
                Width = 80,
                BorderThickness = new Thickness(0),
                Background = new SolidColorBrush(Color.FromRgb(248,248,248)),
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
                RowDefinitions =
                {
                    new RowDefinition()
                    {
                        Height = new GridLength(2, GridUnitType.Star)
                    },
                    new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    }
                }
            };
            TextBlock name = GetTextBlock(recipe.Name, new SolidColorBrush(Colors.Black), FontWeights.Bold);
            TextBlock price = GetTextBlock($"{recipe.Price} cook", new SolidColorBrush(Color.FromRgb(198,198,198)), FontWeights.Thin, 11);

            grid.Children.Add(name);
            grid.Children.Add(price);

            Grid.SetRow(name, 0);
            Grid.SetRow(price, 1);
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
            };
        }
    }

    public enum RecipeRow
    {
        trending,
        cheaper,
        rated,
    }
}

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
    /// Logique d'interaction pour Admin.xaml
    /// </summary>
    public partial class Admin : UserControl
    {
        public Admin()
        {
            InitializeComponent();
            FillGrid();
        }

        private void FillGrid()
        {
            List<Recipe> recipes = GetTop5();
            for (int i = 0;
                i < recipes.Count;
                i ++)
            {
                AddGrid(recipes[i],i);
            }

        }
        private void AddGrid(Recipe recipe, int count)
        {
            Grid grid = GetGrid(recipe);
            TopRecipeGrid.Children.Add(grid);
            Grid.SetRow(grid, count);
        }

        private Grid GetGrid(Recipe recipe)
        {
            Grid grid = new Grid()
            {
                Background = new SolidColorBrush(Colors.Transparent),
                RowDefinitions =
                {
                    new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    },
                    new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    },
                    new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    },
                    new RowDefinition()
                    {
                        Height = new GridLength(1, GridUnitType.Star)
                    },
                },
            };
            TextBlock name = GetTextBlock(recipe.Name, new SolidColorBrush(Colors.White), FontWeights.Bold);
            Client client = GetClient(recipe.NumberCreator);
            TextBlock creator = GetTextBlock(client.Name, new SolidColorBrush(Colors.White), FontWeights.Bold);
            TextBlock price = GetTextBlock(recipe.Price.ToString(), new SolidColorBrush(Colors.White), FontWeights.Bold);
            int numberOrder = GetOrder(recipe.Name);
            TextBlock order = GetTextBlock(recipe.Name, new SolidColorBrush(Colors.White), FontWeights.Bold);

            grid.Children.Add(name);
            grid.Children.Add(creator);
            grid.Children.Add(price);
            grid.Children.Add(order);

            Grid.SetRow(name, 0);
            Grid.SetRow(creator, 1);
            Grid.SetRow(price, 2);
            Grid.SetRow(order, 3);

            return grid;
        }
        private Client GetClient(string numberCreator)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            Client client = ddb.SelectClient(new string[] { "nom" }, new string[] { $"'{numberCreator}'" })[0];
            return client;
        }

        private int GetOrder(string recipeName)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            return ddb.SelectOrder(new string[] { "nomRecette" }, new string[] { $"'{recipeName}'" }).Count();
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

        private List<Recipe> GetTop5()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Recipe> tmp = new List<Recipe>();

            return tmp;
        }
    }
}

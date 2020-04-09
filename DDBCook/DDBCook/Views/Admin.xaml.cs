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
        private RecipeCreator _bestCDR;
        public Admin()
        {
            InitializeComponent();
            LoadComboBox();
            //FillGridTop5();
            //FillGridTopCDR();
        }
        public string BestWeek
        {
            get
            {
                return "bestweek";
            }
        }
        public string BestAll
        {
            get
            {
                return "bestall";
            }
        }
        private void LoadComboBox()
        {
            LoadRecipeComboBox();
            LoadCDRComboBox();
        }
        private void LoadRecipeComboBox()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            recipeCB.ItemsSource = ddb.SelectRecipe();
            recipeCB.SelectedIndex = 0;
        }
        private void LoadCDRComboBox()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Client> clients = new List<Client>();
            List<RecipeCreator> recipeCreators = ddb.SelectRecipeCreator();
            
            foreach(RecipeCreator recipeCreator in recipeCreators)
            {
                clients.Add(ddb.SelectClient(new string[] { "numero" }, new string[] { $"'{recipeCreator.Id}'" })[0]);
            }
            cdrCB.ItemsSource = clients;
            cdrCB.SelectedIndex = 0;
        }
        private void FillGridTop5()
        {
            List<Recipe> recipes = GetTop5();
            for (int i = 0;
                i < recipes.Count;
                i ++)
            {
                AddTop5Grid(recipes[i],i);
            }

        }
        private void FillGridTopCDR()
        {
            List<Recipe> recipes = GetTop5BestCDR();
            for (int i = 0;
                i < recipes.Count;
                i++)
            {
                AddBestCDRGrid(recipes[i], i);
            }
        }
        private List<Recipe> GetTop5BestCDR()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Recipe> tmp = ddb.SelectRecipe(new string[] { "numeroCreateur" }, new string[] { $"'{this._bestCDR.Id}'" });
            SortByOrder(tmp);
            List<Recipe> recipes = new List<Recipe>();
            for (int i = 0;
                i < 5;
                i++)
            {
                recipes.Add(tmp[i]);
            }
            return recipes;
        }
        private void SortByOrder(List<Recipe> recipes)
        {

        }
        private void AddBestCDRGrid(Recipe recipe, int count)
        {
            Grid grid = GetGrid(recipe);
            BestCDRRecipes.Children.Add(grid);
            Grid.SetRow(grid, count);
        }
        private void AddTop5Grid(Recipe recipe, int count)
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

        private void CdrButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private void DeletRecipe(Recipe recipe)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<ProductComposition> productCompositions = ddb.SelectProudctComposition(new string[] { ""});
        }
        private void RecipeButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

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
            FillGridTop5();
            FillGridTopCDR();
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
            List<Recipe> recipes = ddb.SelectRecipe();
            if (recipes.Count > 0)
            {
                recipeCB.ItemsSource = recipes;
                recipeCB.SelectedIndex = 0;
            }
            ddb.Close();
        }
        private void LoadCDRComboBox()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Client> clients = new List<Client>();
            List<RecipeCreator> recipeCreators = ddb.SelectRecipeCreator();

            foreach (RecipeCreator recipeCreator in recipeCreators)
            {
                clients.Add(ddb.SelectClient(new string[] { "numero" }, new string[] { $"'{recipeCreator.Id}'" })[0]);
            }
            if (clients.Count > 0)
            {
                cdrCB.ItemsSource = clients;
                cdrCB.SelectedIndex = 0;
            }

            ddb.Close();
        }
        private void FillGridTop5()
        {
            List<Recipe> recipes = GetTop5Recipes();
            for (int i = 0;
                i < recipes.Count;
                i++)
            {
                AddTop5Grid(recipes[i], i);
            }

        }
        private void FillGridTopCDR()
        {
            List<Recipe> recipes = GetTop5RecipesOfBestCdr();
            for (int i = 0;
                i < recipes.Count;
                i++)
            {
                AddBestCDRGrid(recipes[i], i);
            }
        }
        private List<RecipeCreator> GetTop5BestCDR(int nb = 5)
        {
            List<Recipe> recipes = GetTop5Recipes(-1);
            List<RecipeCreator> top5 = new List<RecipeCreator>();

            DDB ddb = new DDB();

            int cpt = 0;
            nb = (nb == -1) ? ddb.SelectRecipeCreator().Count() : nb;
            while (top5.Count < nb && cpt < recipes.Count)
            {
                RecipeCreator recipeCreator = ddb.SelectRecipeCreator(new string[] { "numero" }, new string[] { "'" + recipes[cpt].NumberCreator + "'" }).First();
                if (Contain(top5,recipeCreator))
                {
                    top5.Add(recipeCreator);
                }
                cpt++;
            }

            ddb.Close();

            return top5;

            bool Contain(List<RecipeCreator> list, RecipeCreator rc)
            {
                foreach (RecipeCreator r in list)
                {
                    if (r.Id.Equals(rc.Id))
                        return false;
                }
                return true;
            }

        }
        private void SortByOrder(List<Recipe> recipes)
        {

        }
        private void AddBestCDRGrid(Recipe recipe, int count)
        {
            Grid grid = GetGrid(recipe);
            BestCDRRecipes.Children.Add(grid);
            Grid.SetColumn(grid, count);
        }
        private void AddTop5Grid(Recipe recipe, int count)
        {
            Grid grid = GetGrid(recipe);
            TopRecipeGrid.Children.Add(grid);
            Grid.SetColumn(grid, count);
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
            Client client = ddb.SelectClient(new string[] { "numero" }, new string[] { $"'{numberCreator}'" })[0];
            ddb.Close();
            return client;
        }

        private int GetOrder(string recipeName)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            int nbOrders = ddb.SelectOrder(new string[] { "nomRecette" }, new string[] { $"'{recipeName}'" }).Count();
            ddb.Close();
            return nbOrders;
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

        private List<Recipe> GetTop5Recipes(int nb = 5)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Recipe> recipes = ddb.SelectRecipe();
            List<Order> orders = ddb.SelectOrder();
            ddb.Close();

            List<List<int>> compteur = new List<List<int>>();
            for (int i = 0; i < recipes.Count; i++)
            {
                compteur.Add(new List<int>());
                compteur[i].Add(0);
                compteur[i].Add(i);
                for (int j = 0; j < orders.Count; j++)
                {
                    if (recipes[i].Name.Equals(orders[j].RecipeName))
                    {
                        compteur[i][0] += 1;
                    }
                }
            }
            compteur.Sort((a, b) => (a[0].CompareTo(b[0])));
            compteur.Reverse();

            List<Recipe> top5 = new List<Recipe>();
            nb = (nb == -1) ? compteur.Count() : nb;
            for (int i = 0; i < nb; i++)
            {
                top5.Add(recipes[compteur[i][1]]);
            }

            return top5;
        }

        private List<Recipe> GetTop5RecipesOfBestCdr(int nb = 5){
            RecipeCreator bestCdr = GetTop5BestCDR(1).First();
            DDB ddb = new DDB();
            List<Recipe> recipes = GetTop5Recipes(-1);
            List<Recipe> top5RecipesOfBestCdr = new List<Recipe>();

            ddb.Close();
            
            nb = (nb == -1) ? recipes.Count() : nb;
            int cpt=0;
            while(top5RecipesOfBestCdr.Count< nb && nb < recipes.Count){
                if (recipes[cpt].NumberCreator.Equals(bestCdr.Id))
                    top5RecipesOfBestCdr.Add(recipes[cpt]);

                cpt++;
            }

            return top5RecipesOfBestCdr ;
        }


        private void CdrButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = cdrCB.SelectedItem as Client;

            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            RecipeCreator recipeCreator = ddb.SelectRecipeCreator(new string[] { "numero" }, new string[] { $"'{client.PhoneNumber}'" })[0];
            ddb.DeleteRecipeCreator(recipeCreator);
            ddb.Close();
            LoadComboBox();
        }

        private void RecipeButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = recipeCB.SelectedItem as Recipe;
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            ddb.DeleteRecipe(recipe);
            ddb.Close();
            LoadComboBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }
    }
}

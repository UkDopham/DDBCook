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
            FillCDROfTheWeek();
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
            TopRecipeGrid.Children.Clear();
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
            BestCDRRecipes.Children.Clear();
            List<Recipe> recipes = GetTop5RecipesOfBestCdr();
            for (int i = 0;
                i < recipes.Count;
                i++)
            {
                AddBestCDRGrid(recipes[i], i);
            }
        }
        private void FillCDROfTheWeek()
        {
            RecipeCreator bestCdrOfWeek = GetTop5BestCDR(1, true).First();
            DDB ddb = new DDB();
            CDRWeekTB.Text = ddb.SelectClient(new string[] { "numero" }, new string[] { $"'{bestCdrOfWeek.Id}'" }).First().Name;
            ddb.Close();
        }
        /// <summary>
        /// Recupere les 5 meilleurs cdr de tout les temps
        /// </summary>
        /// <param name="nb"> nb de cdr retournees (par defaut 5) (-1 si toutes)</param>
        /// <param name="ofWeek"> si true => 5 meilleurs cdr de la semaine </param>
        /// <returns></returns>
        private List<RecipeCreator> GetTop5BestCDR(int nb = 5, bool ofWeek = false)
        {
            DDB ddb = new DDB();

            List<RecipeCreator> listCdr = ddb.SelectRecipeCreator();  // recupere tout les cdr

                                                                                   
            List<Order> orders = new List<Order>();   // recupere les commandes (de la derniere semaine si necessaire)
            if (ofWeek)                             
                orders = ddb.SelectOrder(new string[] { "date" }, new string[] { "NOW()" }, "BETWEEN DATE_SUB(NOW(), INTERVAL 7 DAY) AND");
            else
                orders = ddb.SelectOrder();

            List<int[]> compteur = new List<int[]>(); // compteur stockant le nombre de recettes comandes par cdr
            for (int i = 0; i < listCdr.Count; i++)
            {
                compteur.Add(new int[] { 0, i });
                List<Recipe> listRecipes = ddb.SelectRecipe(new string[] { "numeroCreateur" }, new string[] { $"{listCdr[i].Id}" });

                foreach (Order order in orders)
                {
                    if (ContainRecipe(listRecipes, order)) { compteur[i][0]++; }
                }

            }
            compteur.Sort((a, b) => (a[0].CompareTo(b[0])));
            compteur.Reverse();

            nb = (nb == -1) ? compteur.Count() : nb;
            List<RecipeCreator> top5 = new List<RecipeCreator>();
            for (int i = 0; i < nb; i++)  // recupere le nombre de cdr necessaire
            {
                top5.Add(listCdr[compteur[i][1]]);
            }
           

            ddb.Close();

            return top5;


            bool ContainRecipe(List<Recipe> list, Order o)
            {
                foreach (Recipe r in list)
                {
                    if (r.Name.Equals(o.RecipeName))
                        return true;
                }
                return false;
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
                FontFamily = titleTB.FontFamily,
                Foreground = colorBrush,
            };
        }

        /// <summary>
        /// Recupere les 5 recettes les plus commandes de tout les temps
        /// </summary>
        /// <param name="nb"> nb de recettes retournees (par defaut 5) (-1 si toutes)</param>
        /// <param name="ofWeek"> si true => 5 meilleurs recettes de la semaine </param>
        /// <returns></returns>
        private List<Recipe> GetTop5Recipes(int nb = 5, bool ofWeek = false)
        {
            ///RECUPERATION DES COMMANDES
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Recipe> recipes = ddb.SelectRecipe(); // recupere toutes les recettes

            List<Order> orders = new List<Order>(); // recupere les commandes
            if (ofWeek)                             // (des 7 derniers jours si true)
                orders = ddb.SelectOrder(new string[] { "date" }, new string[] { "NOW()" }, "BETWEEN DATE_SUB(NOW(), INTERVAL 7 DAY) AND");
            else
                orders = ddb.SelectOrder();         // (sinon de tout)

            ddb.Close();

            ///CALCUL DU NOMBRE DE FOIS QUE CHAQUE RECETTES A ETE COMMANDE
            List<List<int>> compteur = new List<List<int>>(); // compteur sur le nombre d apparitions de chaque recette
                                                              //      [nombre d'apparition][indexe recette (par rapport a liste recipe)]
                                                              // [0] (exemple:    3                       0       
                                                              // [1]              2                       1   )
                                                              // ...
            for (int i = 0; i < recipes.Count; i++)
            {
                compteur.Add(new List<int>());
                compteur[i].Add(0);  // compteur (debut 0)
                compteur[i].Add(i);  // indexe recette
                for (int j = 0; j < orders.Count; j++)
                {
                    if (recipes[i].Name.Equals(orders[j].RecipeName))   // si on trouve une recette dans les commandes
                    {
                        compteur[i][0] += 1;                            // compteur + 1
                    }
                }
            }
            compteur.Sort((a, b) => (a[0].CompareTo(b[0]))); // on trie la colonne par rapport a la colonne des compteurs  ([0])
            compteur.Reverse();                              // on dispose les resultats dans l ordre decroissant

            ///RECUPERATION DES MEILLEURS RECETTES
            nb = (nb == -1) ? compteur.Count() : nb;    // recupere la de nb (si nb==-1  nb = le nombre total de recettes)
            List<Recipe> top5 = new List<Recipe>();
            for (int i = 0; i < nb; i++)                // on garde seulement les 'nb' premieres recettes
            {
                top5.Add(recipes[compteur[i][1]]);
            }

            return top5;
        }
        /// <summary>
        /// Recupere les 5 meilleurs recettes du meilleur cdr
        /// </summary>
        /// <param name="nb"></param>
        /// <returns></returns>
        private List<Recipe> GetTop5RecipesOfBestCdr(int nb = 5, bool ofWeek = false)
        {
            RecipeCreator bestCdr = GetTop5BestCDR(1, ofWeek).First(); // recupere le meilleur cdr
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            BestCDRAllTB.Text = ddb.SelectClient(new string[] { "numero" }, new string[] { $"'{bestCdr.Id}'" })[0].Name;
            List<Recipe> recipes = GetTop5Recipes(-1);                 // recupere les meilleurs recettes
            List<Recipe> top5RecipesOfBestCdr = new List<Recipe>(); 

            ddb.Close();

            nb = (nb == -1) ? recipes.Count() : nb;
            int cpt = 0;
            while (top5RecipesOfBestCdr.Count < nb && cpt < recipes.Count) // recupre les nb premieres
            {
                if (recipes[cpt].NumberCreator.Equals(bestCdr.Id))
                    top5RecipesOfBestCdr.Add(recipes[cpt]);

                cpt++;
            }

            return top5RecipesOfBestCdr;
        }


        private void CdrButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = cdrCB.SelectedItem as Client;

            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            RecipeCreator recipeCreator = ddb.SelectRecipeCreator(new string[] { "numero" }, new string[] { $"'{client.PhoneNumber}'" })[0];
            ddb.DeleteRecipeCreator(recipeCreator);
            ddb.Close();
            LoadComboBox();
            FillGridTop5();
            FillGridTopCDR();
            FillCDROfTheWeek();
        }

        private void RecipeButton_Click(object sender, RoutedEventArgs e)
        {
            Recipe recipe = recipeCB.SelectedItem as Recipe;
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            ddb.DeleteRecipe(recipe);
            ddb.Close();
            LoadComboBox();
            FillGridTop5();
            FillGridTopCDR();
            FillCDROfTheWeek();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }
    }
}

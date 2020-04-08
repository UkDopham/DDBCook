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
        public string Balance
        {
            get
            {
                return $"{this._client.Money} cook";
            }
        }
        public ClientInformation(Client client)
        {
            InitializeComponent();
            this._client = client;
            InitilizationRecipes();
        }
        
        private void InitilizationRecipes()
        {
            RecipeStackPanel.Children.Clear();
            //Get recipes created by the client
            
            List<Recipe> recipes = new List<Recipe>();

            recipes.Add(new Recipe("couscous", Models.Enums.RecipeType.boisson, "desc couscousdesc couscousdesc couscousdesc couscousdesc couscous", "John doe"));
            recipes.Add(new Recipe("couscous", Models.Enums.RecipeType.boisson, "desc couscous", "John doe"));
            recipes.Add(new Recipe("couscous", Models.Enums.RecipeType.boisson, "desc couscous", "John doe"));
            recipes.Add(new Recipe("couscous", Models.Enums.RecipeType.boisson, "desc couscous", "John doe"));
            recipes.Add(new Recipe("couscous", Models.Enums.RecipeType.boisson, "desc couscous", "John doe"));

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
                Background = new SolidColorBrush(Color.FromRgb(248, 248, 248)),
                Height = 80,
                Width = 400,
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
            TextBlock name = GetTextBlock(recipe.Name, new SolidColorBrush(Color.FromRgb(112, 111, 211)), FontWeights.Bold, 26);
            TextBlock desc = GetTextBlock(recipe.Description, new SolidColorBrush(Color.FromRgb(218, 218, 218)), FontWeights.Bold, 14);
            grid.Children.Add(name);
            grid.Children.Add(desc);

            Grid.SetRow(name, 0);
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

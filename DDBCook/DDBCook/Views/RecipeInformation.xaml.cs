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
    /// Logique d'interaction pour RecipeInformation.xaml
    /// </summary>
    public partial class RecipeInformation : UserControl
    {
        private Recipe _recipe;

        public Visibility IsHealthy
        {
            get
            {
                return this._recipe.IsHealthy ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public Visibility IsBio
        {
            get
            {
                return this._recipe.IsBio ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility IsTrending
        {
            get
            {
                return this._recipe.IsTrending ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility IsChimical
        {
            get
            {
                return this._recipe.IsChimical ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public Visibility IsVegan
        {
            get
            {
                return this._recipe.IsVegan ? Visibility.Visible : Visibility.Hidden;
            }
        }
        public string RecipePrice
        {
            get
            {
                return $"{this._recipe.Price} cook";
            }
        }
        public Recipe Recipe
        {
            get
            {
                return this._recipe;
            }
        }
        public RecipeInformation(Recipe recipe)
        {
            InitializeComponent();
            this._recipe = recipe;
            InitilizationProduct();
        }

        

        /// <summary>
        /// Show the recipe products
        /// </summary>
        private void InitilizationProduct()
        {
            //Request sql for the products
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<ProductComposition> products = ddb.SelectProductComposition(new string[] { "nomRecette" }, new string[] { $"'{this._recipe.Name}'" });
            foreach (ProductComposition product in products)
            {
                AddProduct(product);
            }
        }

        private void AddProduct(ProductComposition product)
        {
            ProductsStackPanel.Children.Add(GetBorder(product));
        }

        private Border GetBorder(ProductComposition product)
        {
            return new Border()
            {
                Background = new SolidColorBrush(Color.FromRgb(112, 111, 211)),
                Child = GetTextBlock(product, new SolidColorBrush(Colors.White), FontWeights.Bold, 14),
                BorderThickness = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)               
            };
        }
        private TextBlock GetTextBlock(ProductComposition productCompo, SolidColorBrush colorBrush, FontWeight fontWeight, int fontSize = 12)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            Product product = ddb.SelectProduct(new string[] { "ref" }, new string[] { $"'{productCompo.RefProduct}'" })[0];
            string text = productCompo.RefProduct;
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
                Text = $"{product.Name} {productCompo.Quantity}{product.Unit}",
                Margin = new Thickness(10),
                FontSize = fontSize,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent),
                FontWeight = fontWeight,
                Foreground = colorBrush,
                FontFamily = titleTB.FontFamily
            };
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            //Add the recipe to the basket;
            Basket.Recipes.Add(this.Recipe);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }
    }
}

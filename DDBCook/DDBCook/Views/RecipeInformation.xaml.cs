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
            List<Product> products = new List<Product>();
            products.Add(new Product("Banane", Models.Enums.ProductCategory.eauGlace, 1, 1, 1, "ZZ", "ZZZ", "Kg"));
            products.Add(new Product("Banane", Models.Enums.ProductCategory.eauGlace, 1, 1, 1, "ZZ", "ZZZ", "Kg"));
            products.Add(new Product("Banane", Models.Enums.ProductCategory.eauGlace, 1, 1, 1, "ZZ", "ZZZ", "Kg"));
            foreach (Product product in products)
            {
                AddProduct(product);
            }
        }

        private void AddProduct(Product product)
        {
            ProductsStackPanel.Children.Add(GetBorder(product));
        }

        private Border GetBorder(Product product)
        {
            return new Border()
            {
                Background = new SolidColorBrush(Color.FromRgb(112, 111, 211)),
                Child = GetTextBlock(product.Name, new SolidColorBrush(Colors.White), FontWeights.Bold, 14),
                BorderThickness = new Thickness(0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(5)               
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
                Margin = new Thickness(10),
                FontSize = fontSize,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent),
                FontWeight = fontWeight,
                Foreground = colorBrush,
            };
        }

        private void ShopButton_Click(object sender, RoutedEventArgs e)
        {
            //Add the recipe to the basket;
            Basket.Recipes.Add(this.Recipe);
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }
    }
}

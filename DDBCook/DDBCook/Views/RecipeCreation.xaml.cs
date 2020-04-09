using DDBCook.Models;
using DDBCook.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Logique d'interaction pour RecipeCreation.xaml
    /// </summary>
    public partial class RecipeCreation : UserControl
    {
        private List<Product> _allProducts = new List<Product>();
        private List<DoubleContainer<Product, ProductComposition>> _products = new List<DoubleContainer<Product, ProductComposition>>();
        private List<DoubleContainer<string, RecipeType>> _recipeTypes = new List<DoubleContainer<string, RecipeType>>();
        public RecipeCreation()
        {
           //DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
           //User.ConnectedClient = ddb.SelectClient()[0];
            InitializeComponent();
            LoadProducts();
            InitializeProducts();
            LoadRecipeType();
        }
        private void LoadProducts()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            this._allProducts = ddb.SelectProduct();

            ProductsComboBox.ItemsSource = this._allProducts;
            ProductsComboBox.SelectedIndex = 0;
        }
        private void LoadRecipeType()
        {
            this._recipeTypes.Add(new DoubleContainer<string, RecipeType>("plat", RecipeType.plat));
            this._recipeTypes.Add(new DoubleContainer<string, RecipeType>("boisson", RecipeType.boisson));
            this._recipeTypes.Add(new DoubleContainer<string, RecipeType>("dessert", RecipeType.dessert));

            CategoryComboBox.ItemsSource = this._recipeTypes;
            CategoryComboBox.SelectedIndex = 0;
        }
        private void InitializeProducts()
        {
            ProductsStackPanel.Children.Clear();
            foreach(DoubleContainer<Product, ProductComposition> productComposition in this._products)
            {
                AddProduct(productComposition);
            }
        }

        private void AddProduct(DoubleContainer<Product, ProductComposition> productComposition)
        {
            ProductsStackPanel.Children.Add(GetBorder(productComposition));
        }

        private Border GetBorder(DoubleContainer<Product, ProductComposition> product)
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
        private TextBlock GetTextBlock(DoubleContainer<Product, ProductComposition> product, SolidColorBrush colorBrush, FontWeight fontWeight, int fontSize = 12)
        {
            string text = product.Value.Name;
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
                Text = $"{text} {product.OtherValue.Quantity}{product.Value.Unit}",
                Margin = new Thickness(10),
                FontSize = fontSize,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Background = new SolidColorBrush(Colors.Transparent),
                FontWeight = fontWeight,
                Foreground = colorBrush,
            };
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ProductsComboBox_Selected(object sender, RoutedEventArgs e)
        {
            Product product = ((ComboBox)sender).SelectedItem as Product;
            UnitTextBlock.Text = product.Unit;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            Product product = ProductsComboBox.SelectedItem as Product;
            this._products.Add(new DoubleContainer<Product, ProductComposition>(product,new ProductComposition(Guid.NewGuid().ToString(), Convert.ToInt32(QuantityTextBox.Text), product.Reference, NameTextBox.Text)));
            InitializeProducts();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            if (ddb.SelectRecipeCreator(new string[] { "numero" }, new string[] { $"'{User.ConnectedClient.PhoneNumber}'" }).Count == 0) //if is not a cdr
            {
                ddb.InsertRecipeCreator(User.ConnectedClient.PhoneNumber);
            }
            DoubleContainer<string, RecipeType> recipeType = CategoryComboBox.SelectedItem as DoubleContainer<string, RecipeType>;
            ddb.InsertRecipe(NameTextBox.Text, recipeType.OtherValue, DescTextBox.Text, User.ConnectedClient.PhoneNumber, Convert.ToInt32(PriceTextBox.Text), 
                HealthyCB.IsChecked == true ? true : false,
                BioCB.IsChecked == true ? true : false,
                VeganCB.IsChecked == true ? true : false,
                ChimiCB.IsChecked == true ? true : false); //operator ter because it's bool? not bool

            foreach(DoubleContainer<Product, ProductComposition> productComposition in this._products) // if the user change the name after adding the product composition
            {
                productComposition.OtherValue.RecipeName = NameTextBox.Text;
                ddb.Insert<ProductComposition>(productComposition.OtherValue);
            }
            ddb.Close();
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }
    }

    
}

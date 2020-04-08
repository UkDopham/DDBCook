using DDBCook.Models;
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
        private List<ProductComposition> _allProducts = new List<ProductComposition>();
        private List<ProductComposition> _products = new List<ProductComposition>();
        public RecipeCreation()
        {
            InitializeComponent();
            LoadProducts();
            InitializeProducts();
        }
        private void LoadProducts()
        {
            DDB ddb = new DDB("cook", "root", "alexandre1");
            this._allProducts = ddb.SelectProudctComposition();
            ProductsComboBox.ItemsSource = this._allProducts;
        }
        
        private void InitializeProducts()
        {
            ProductsStackPanel.Children.Clear();
            foreach(ProductComposition productComposition in this._products)
            {
                AddProduct(productComposition);
            }
        }

        private void AddProduct(ProductComposition productComposition)
        {
            ProductsStackPanel.Children.Add(GetBorder(productComposition));
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
        private TextBlock GetTextBlock(ProductComposition product, SolidColorBrush colorBrush, FontWeight fontWeight, int fontSize = 12)
        {
            string text = product.RefProduct;
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
                Text = $"{text} x{product.Quantity}",
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
    }
}

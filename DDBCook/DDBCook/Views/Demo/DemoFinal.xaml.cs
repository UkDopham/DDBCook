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

namespace DDBCook.Views.Demo
{
    /// <summary>
    /// Logique d'interaction pour DemoFinal.xaml
    /// </summary>
    public partial class DemoFinal : UserControl
    {
        public DemoFinal()
        {
            InitializeComponent();
            LoadProduct();
        }
        private void LoadProduct()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Product> products = ddb.SelectProduct(new string[] { "quantite_actuelle" }, new string[] { $"quantite_min * 2" }, ">");
            ProductComboBox.ItemsSource = products;
            if (products.Count > 0)
            {
                ProductComboBox.SelectedIndex = 0;
            }
            ddb.Close();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow.DataContext = new MainMenu();
        }

        private void ProductComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Product product = ProductComboBox.SelectedValue as Product;

            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<ProductComposition> productCompositions = ddb.SelectProductComposition(new string[] { "refProduit" }, new string[] { $"'{product.Reference}'" });
            List<Recipe> recipes = new List<Recipe>();
            foreach(ProductComposition productComposition in productCompositions)
            {
                List<Recipe> tmp = ddb.SelectRecipe(new string[] { "nom" }, new string[] { $"'{productComposition.RecipeName}'" });
                if (tmp.Count > 0)
                {
                    recipes.Add(tmp[0]);
                }
            }
            contentStackPanel.Children.Clear();
            foreach(Recipe recipe in recipes)
            {
                contentStackPanel.Children.Add(GetTextBlock(recipe.Name, new SolidColorBrush(Colors.Green), FontWeights.Bold));
            }
            ddb.Close();
        }
    }
}

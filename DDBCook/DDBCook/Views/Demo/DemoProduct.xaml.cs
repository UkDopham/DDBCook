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
    /// Logique d'interaction pour DemoProduct.xaml
    /// </summary>
    public partial class DemoProduct : UserControl
    {
        public DemoProduct()
        {
            InitializeComponent();
            LoadProduct();
        }
        private void LoadProduct()
        {
            DDB ddb = new DDB(User.DataBase, User.Username, User.Password);
            List<Product> products = ddb.SelectProduct(new string[] { "quantite_actuelle" }, new string[] { $"quantite_min * 2" }, ">");
            foreach(Product product in products)
            {
                contentStackPanel.Children.Add(GetTextBlock($"{product.Name} current :{product.CurrentQuantity}, min :{product.MinQuantity}", new SolidColorBrush(Colors.Green), FontWeights.Bold));
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
            mainWindow.DataContext = new DemoFinal();
        }
    }
}

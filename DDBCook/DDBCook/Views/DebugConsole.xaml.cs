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
using System.Windows.Shapes;

namespace DDBCook.Views
{
    /// <summary>
    /// Logique d'interaction pour DebugConsole.xaml
    /// </summary>
    public partial class DebugConsole : Window
    {
        public DebugConsole()
        {
            InitializeComponent();
           // this.Topmost = true;
        }
        public void AddNewLine(string lineText)
        {
            TextBlock textBlock = new TextBlock()
            {
                Text = lineText,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.Green)
            };
            consoleStackPanel.Children.Add(textBlock);
        }
    }

    
}

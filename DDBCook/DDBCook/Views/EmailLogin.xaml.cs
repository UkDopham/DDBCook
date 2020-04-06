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
    /// Logique d'interaction pour EmailLogin.xaml
    /// </summary>
    public partial class EmailLogin : UserControl
    {
        private RegisterOrLogin _registerOrLogin;
        public EmailLogin(RegisterOrLogin registerOrLogin)
        {
            InitializeComponent();
            this._registerOrLogin = registerOrLogin;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //check if email exists
        }
    }
}

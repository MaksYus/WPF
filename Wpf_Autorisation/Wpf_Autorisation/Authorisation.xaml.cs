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

namespace Wpf_Autorisation
{
    /// <summary>
    /// Логика взаимодействия для Authorisation.xaml
    /// </summary>
    public partial class Authorisation : Window
    {
        MainWindow parent;
        public Authorisation(MainWindow par)
        {
            InitializeComponent();
            parent = par;
            this.Top = par.Top;
            this.Left = par.Left;
        }
        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            parent.Show();
            this.Close();
        }
        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text;
            var pass = PassBox.Password;
            bool f = true;
            if (login.Length < 5)
            {
                LoginBox.ToolTip = "Минимальная длина - 5 символов!";
                LoginBox.Background = Brushes.Red;
                f = false;
            }
            else
            {
                LoginBox.ToolTip = "успешно"; LoginBox.Background = Brushes.LightGreen;
            }
            if (pass.Length < 8)
            {
                PassBox.ToolTip = "Минимальная длина - 8 символов!";
                PassBox.Background = Brushes.Red;
                f = false;
            }
            else
            {
                PassBox.ToolTip = "успешно";
                PassBox.Background = Brushes.LightGreen;
            }
            if (f)
            {
                UserInfo user = parent.GetFromDB(login);
                MessageBox.Show((user.Password == pass).ToString());
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
using System.Data;
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
using System.IO;

namespace Wpf_Autorisation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void WriteInDB(UserInfo user)
        {
            string command, connectionString = "data source = MySchedule.db;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
                    SQLiteCommand sqlCommand;
            if (!File.Exists("MySchedule.db"))
            {
                var answ = MessageBox.Show("База данных отсутствует. Создать новую базу?", "Error",MessageBoxButton.YesNo);
                if (answ == MessageBoxResult.Yes)
                {
                    SQLiteConnection.CreateFile("MySchedule.db");
                    connection.Open();
                    command = string.Format("create table 'Users' (login text, mail text, password text)");
                    sqlCommand = new SQLiteCommand(command, connection);
                    sqlCommand.ExecuteNonQuery();
                    connection.Close();
                }
            }
            connection.Open();
            command = string.Format("UPDATE 'Users' SET mail = '{1}', password = '{2}' WHERE login = '{0}'", user.Login,user.Mail,user.Password);
            sqlCommand = new SQLiteCommand(command, connection);
            int rows = sqlCommand.ExecuteNonQuery();
            if (rows == 0)
            {
                command = string.Format("insert into 'Users' (login, mail, password) values ('{0}', '{1}', '{2}')", user.Login, user.Mail, user.Password);
                sqlCommand = new SQLiteCommand(command, connection);
                sqlCommand.ExecuteNonQuery();
            }
            connection.Close();
        }
        public UserInfo GetFromDB(string login)
        {
            UserInfo user = new UserInfo();
            string command, connectionString = "data source = MySchedule.db;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            SQLiteCommand sqlCommand;
            connection.Open();
            command = string.Format("SELECT * FROM 'Users' WHERE login = '{0}'", login);
            sqlCommand = new SQLiteCommand(command, connection);
            try
            {
            sqlCommand.ExecuteNonQuery();
            using (var reader = sqlCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    var mail = reader.GetString(1);
                    var pass = reader.GetString(2);
                    user = new UserInfo(login, mail, pass);
                }
            }
            }
            catch 
            {
                MessageBox.Show("Пользователя с логином '"+ login+"' не найдено", "Error");
            }
            
            return user;
        }
        private void Button_Registration_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text;
            var pass = PassBox.Password;
            var repass = RePassBox.Password;
            var mail = EmailBox.Text.ToLower();
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
            if (repass != pass || repass.Length == 0)
            {
                RePassBox.ToolTip = "Пароль должен повториться";
                RePassBox.Background = Brushes.Red;
                f = false;
            }
            else
            {
                RePassBox.ToolTip = "успешно";
                RePassBox.Background = Brushes.LightGreen;
            }
            if (mail.IndexOf('@') < 1 || mail.LastIndexOf('.') < mail.IndexOf('@'))
            {
                EmailBox.ToolTip = "Логин неправильной конструкции";
                EmailBox.Background = Brushes.Red;
                f = false;
            }
            else
            {
                EmailBox.ToolTip = "успешно";
                EmailBox.Background = Brushes.LightGreen;
            }
            if (f)
            {
                UserInfo user = new UserInfo(login, mail, pass);
                WriteInDB(user);
                sign_in.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }
        private void Button_auth_click(object sender, RoutedEventArgs e)
        {
            Authorisation authorisation = new Authorisation(this);
            authorisation.Show();
            Hide();
        }
    }
}

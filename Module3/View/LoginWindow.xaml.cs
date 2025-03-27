using Module3.Model;
using Module3.View;
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

namespace Module3
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        int count = 0;
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            var user = new User("Pupkin", "Vasya", "admin", "1234", "Администратор");
            
            if (LoginTextBox.Text.Equals("") || LoginPasswordBox.Password.Equals(""))
            {
                MessageBox.Show("Заполнены не все поля!", "Предупреждение", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                count++;
                if (count > 3)
                {
                    MessageBox.Show("Вход заблокирован. Пожалуйста, обратитесь к администратору для разблокировки.",
                        "Блокирование", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    LoginButton.IsEnabled = false;
                } 
            }
            else if (LoginTextBox.Text.Equals(user.Login) && LoginPasswordBox.Password.Equals(user.Password))
            {
                MessageBox.Show("Добро пожаловать!", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                AdministratorWindow window = new AdministratorWindow();
                window.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль. Попробуйте ещё раз.", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

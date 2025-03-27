using Module3.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace Module3.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        bool success = false;
        Database database = new Database();
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RigestrateButtonClick(object sender, RoutedEventArgs e)
        {
            success = RegistrationSuccess() && CheckData() && CheckDatabase();
            if (success)
            {
                AddUserToDatabase();

                MessageBox.Show("Регистрация успешна!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void AddUserToDatabase()
        {
            var surname = SurnameTextBox.Text;
            var name = NameTextBox.Text;
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Password;
            var role = RoleComboBox.SelectedIndex == 0 ? "Пользователь" : "Администратор";
            var date = DateTime.Now;

            string query = $"INSERT INTO users(surname, name, login, password, role, is_blocked, last_enter_date) " +
                $"VALUES('{surname}', '{name}', '{login}', '{password}', '{role}', 'false', '{date}')";

            try
            {
                SqlCommand command = new SqlCommand(query, database.GetSqlConnection());
                database.OpenConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Данные успешно добавлены в БД!", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool RegistrationSuccess()
        {
            var formFullness = !SurnameTextBox.Text.Equals("") ||
                !NameTextBox.Text.Equals("") ||
                !LoginTextBox.Text.Equals("") ||
                !PasswordTextBox.Password.Equals("");

            if (formFullness)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполнены не все поля!", "Предупреждение", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }
        }

        private bool CheckData()
        {
            var forbiddenSymbols = !Regex.IsMatch(SurnameTextBox.Text, @"^[a-zA-Z\s]*$") ||
                !Regex.IsMatch(NameTextBox.Text, @"^[a-zA-Z\s]*$") ||
                !Regex.IsMatch(LoginTextBox.Text, @"^[a-zA-Z\s]*$");

            if (forbiddenSymbols)
            {
                MessageBox.Show("Поля Фамилия, Имя или Логин содержат запрещённые символы!",
                    "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool CheckDatabase()
        {
            var login = LoginTextBox.Text;
            string query = $"SELECT login FROM users WHERE login = '{login}'";

            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable table = new DataTable();

            try
            {
                SqlCommand command = new SqlCommand(query, database.GetSqlConnection());

                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count >= 1)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!",
                        "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}

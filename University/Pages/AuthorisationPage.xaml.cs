using System;
using System.Collections.Generic;
using System.IO;
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
using University.Data;

namespace University.Pages
{
    public partial class AuthorisationPage : Page
    {
        private List<Employee> _employees;
        public AuthorisationPage()
        {
            InitializeComponent();
            _employees = DatabaseHelper.GetEmployees();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            int password = 0;
            if (String.IsNullOrEmpty(txtLogin.Text) || String.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            Employee newEmp = _employees.FirstOrDefault(x => x.Fam == login);
            if (newEmp == null)
            {
                MessageBox.Show("Сотрудник с данным логином не найден.");
                txtLogin.Clear();
                txtPassword.Clear();
                return;
            }

            try
            {
                password = Convert.ToInt32(txtPassword.Password);
            }
            catch
            {
                MessageBox.Show("Пароль должен быть числом!");
                txtPassword.Password = "";
                return;
            }

            if (password == newEmp.Numder)
            {
                NavigationService.Navigate(new MainPage(newEmp));
            }
            else
            {
                MessageBox.Show("Неверный пароль.");
                txtPassword.Password = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            QRWindow qrWindow = new QRWindow();

            // Отображаем новое окно
            qrWindow.Show();

        }
    }
}
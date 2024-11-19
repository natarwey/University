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
        private static DataBaseContext _connection = new DataBaseContext();
        public AuthorisationPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var login = txtLogin.Text;
            var password = txtPassword.Password;

            // TODO можно прикрутить Regex для проверки заполнения полей

            if (String.IsNullOrEmpty(txtLogin.Text) || String.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }

            var people = _connection.People.FirstOrDefault(x => x.login == login && x.password == password);
            if (people is null)
            {
                MessageBox.Show("Не правильный логин или пароль!");
                txtPassword.Clear();
                return;
            }

            if(people.role == 1)
            {
                var student = _connection.Student.FirstOrDefault(x => x.id_people == people.id);

                NavigationService.Navigate(new MainPageByStudent(student, people));

            }
            else if(people.role == 2)
            {

                var employe = _connection.Employe.FirstOrDefault(x => x.id_people == people.id);

                NavigationService.Navigate(new MainPageByEmploye(employe, people));
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
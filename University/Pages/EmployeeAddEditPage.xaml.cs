using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// <summary>
    /// Логика взаимодействия для EmployeeAddEditPage.xaml
    /// </summary>
    public partial class EmployeeAddEditPage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private Employe _employe;
        private readonly People _people;

        public EmployeeAddEditPage(Employe employe, People people)
        {
            InitializeComponent();
            _employe = employe;
            _people = people;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            var fio = _connection.People.Where(x => x.role == 2).ToList();
            FioBox.ItemsSource = fio;
            FioBox.DisplayMemberPath = "id";
            var depart = _connection.Department.ToList();
            DepartBox.ItemsSource = depart;
            DepartBox.DisplayMemberPath = "name";
            if (_employe != null)
            {
                TextBoxFio.Text = "Редактирование сотрудника";
                //FioBox.Text = _employe.id_people.ToString();
                var index = fio.Select(x => x.id).ToList().IndexOf(_employe.People.id);
                FioBox.SelectedIndex = index;
                SalaryBox.Text = _employe.salary.ToString();
                PostBox.Text = _employe.post;
                StazhBox.Text = _employe.stazh.ToString();
                var index2 = depart.Select(x => x.id).ToList().IndexOf(_employe.Department.id);
                DepartBox.SelectedIndex = index2;
            }
            else
            {
                TextBoxFio.Text = "Создание сотрудника";
            }
        }

        private void SalaryBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9,]", "");
        }
        private void StazhBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9,]", "");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FioBox.Text))
            {
                MessageBox.Show("Пожалуйста, выберите id сотрудника.");
                return;
            }
            else
            {
                if (_employe is null)
                {
                    // когда создание
                    _employe = new Employe();
                    //_employe.People.Fio = FioBox.Text;
                    _employe.id_people = (FioBox.SelectedItem as People).id;
                    _employe.salary = int.TryParse(SalaryBox.Text, out var value) ? value : 0;
                    _employe.post = PostBox.Text;
                    _employe.stazh = int.TryParse(StazhBox.Text, out var value2) ? value2 : 0;
                    _employe.id_department = (DepartBox.SelectedItem as Department).id;

                    using (var context = new DataBaseContext())
                    {
                        _connection.Employe.Add(_employe);
                        _connection.SaveChanges();
                    }

                    MessageBox.Show("Сотруднк сохранен!");
                    NavigationService.GoBack();
                    //NavigationService.RemoveBackEntry();
                    
                }
                else
                {
                    // когда редактирование
                    //_employe.id_people = _people.id;
                    _employe.id_people = (FioBox.SelectedItem as People).id;
                    _employe.salary = int.TryParse(SalaryBox.Text, out var value) ? value : 0;
                    _employe.post = PostBox.Text;
                    _employe.stazh = int.TryParse(StazhBox.Text, out var value2) ? value2 : 0;
                    _employe.id_department = (DepartBox.SelectedItem as Department).id;

                    using (var context = new DataBaseContext())
                    {
                        _connection.SaveChanges();
                    }

                    MessageBox.Show("Сотруднк сохранен!");
                    NavigationService.GoBack();
                    //NavigationService.RemoveBackEntry();
                }
            }
            _connection.SaveChanges();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}

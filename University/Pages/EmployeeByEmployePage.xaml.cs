using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    /// <summary>
    /// Логика взаимодействия для EmployeeByEmployePage.xaml
    /// </summary>
    public partial class EmployeeByEmployePage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private Employe _employe;
        private readonly People _people;

        public EmployeeByEmployePage(Employe employe)
        {
            InitializeComponent();
            _employe = employe;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            var emp = _connection.Employe
                .Where(x => _employe.id == x.id_department)
                .ToArray();


            dataEmploye.ItemsSource = emp.Select(x => new EmployeeViwe
            {
                id = x.id,
                fio = x.People.Fio,
                salary = x.salary,
                post = x.post,
                stazh = x.stazh,
                department = x.Department.name
            });
        }

        private void SerchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var emp = _connection.Employe
                .Where(x => _employe.id == x.id_department)
                .Where(x => x.People.Fio.ToUpper().Contains(SerchBox.Text.ToUpper()))
                .ToArray();


            dataEmploye.ItemsSource = emp.Select(x => new EmployeeViwe
            {
                id = x.id,
                fio = x.People.Fio,
                salary = x.salary,
                post = x.post,
                stazh = x.stazh,
                department = x.Department.name
            });
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeAddEditPage(null, _people));
        }

        private void DeletedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_employe != null)
                {
                    using (var context = new DataBaseContext())
                    {
                        var employeeToDelete = context.Employe.Find(_employe.id);
                        if (employeeToDelete != null)
                        {
                            context.Employe.Remove(employeeToDelete);
                            context.SaveChanges();
                            Load_Student(sender, e);
                            MessageBox.Show("Сотрудник удален.", "Удачно", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Сотрудник не найден.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите сотрудника.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении сотрудника: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(_employe is null)
            {
                MessageBox.Show("Выберите дисциплину!");
            }
            else
            {
                NavigationService.Navigate(new EmployeeAddEditPage(_employe, _people));
            }
        }

        private void dataEmploye_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            var rowView = dataGrid.SelectedItem as EmployeeViwe;
            if (rowView is null)
                return;
            _employe = _connection.Employe.FirstOrDefault(x => x.id == rowView.id);
        }

        private class EmployeeViwe
        {
            public int id { get; set; }
            public string fio { get; set; }
            public long salary { get; set; }
            public string post { get; set; }
            public int stazh { get; set; }
            public string department { get; set; }
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void SortAscButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedEmployees = _connection.Employe
                .Where(x => _employe.id == x.id_department)
                .OrderBy(x => x.salary)
                .ToArray();

            dataEmploye.ItemsSource = sortedEmployees.Select(x => new EmployeeViwe
            {
                id = x.id,
                fio = x.People.Fio,
                salary = x.salary,
                post = x.post,
                stazh = x.stazh,
                department = x.Department.name
            });
        }

        private void SortDescButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedEmployees = _connection.Employe
                .Where(x => _employe.id == x.id_department)
                .OrderByDescending(x => x.salary)
                .ToArray();

            dataEmploye.ItemsSource = sortedEmployees.Select(x => new EmployeeViwe
            {
                id = x.id,
                fio = x.People.Fio,
                salary = x.salary,
                post = x.post,
                stazh = x.stazh,
                department = x.Department.name
            });
        }
    }
}

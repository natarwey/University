using System;
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
    /// Логика взаимодействия для DisciplinesByEmployePage.xaml
    /// </summary>
    public partial class DepartamentPage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private Department _department;
        private readonly Employe _employe;

        public DepartamentPage(Employe employe)
        {
            InitializeComponent();
            _employe = employe;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            var dep = _connection.Department  
                .ToArray();


            dataDepartament.ItemsSource = dep.Select(x => new DepartamentViwe
            {
                id = x.id,
                name = x.name,
            });
        }

        private void SerchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var dep = _connection.Department
                .Where(x => x.name.ToUpper().Contains(SerchBox.Text.ToUpper()))
                .ToArray();


            dataDepartament.ItemsSource = dep.Select(x => new DepartamentViwe
            {
                id = x.id,
                name = x.name,
            });
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepartamentAddEditPage(null, _employe));
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_department is null)
            {
                MessageBox.Show("Выберите кафедру!");
            }
            else
            {
                NavigationService.Navigate(new DepartamentAddEditPage(_department, _employe));
            }
        }


        private void DeletedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_department != null)
                {
                    using (var context = new DataBaseContext())
                    {
                        var departamentToDelete = context.Department.Find(_department.id);
                        if (departamentToDelete != null)
                        {
                            context.Department.Remove(departamentToDelete);
                            context.SaveChanges();
                            Load_Student(sender, e);
                            MessageBox.Show("Кафедра удалена.", "Удача", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Кафедра не найдена.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите кафедру.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении кафедры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dataDiscipline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            var rowView = dataGrid.SelectedItem as DepartamentViwe;
            if (rowView is null)
                return;
            _department = _connection.Department.FirstOrDefault(x => x.id == rowView.id);
        }

        private class DepartamentViwe
        {
            public int id { get; set; }
            public string name { get; set; }
            public string groupName { get; set; }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void SortAscButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedDepartments = _connection.Department
                .OrderBy(x => x.name)
                .ToArray();

            dataDepartament.ItemsSource = sortedDepartments.Select(x => new DepartamentViwe
            {
                id = x.id,
                name = x.name,
            });
        }

        private void SortDescButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedDepartments = _connection.Department
                .OrderByDescending(x => x.name)
                .ToArray();

            dataDepartament.ItemsSource = sortedDepartments.Select(x => new DepartamentViwe
            {
                id = x.id,
                name = x.name,
            });
        }
    }
}

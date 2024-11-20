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
    public partial class DisciplinesByEmployePage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private Discipline _discipline;
        private readonly Employe _employe;

        public DisciplinesByEmployePage(Employe employe)
        {
            InitializeComponent();
            _employe = employe;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            var disp = _connection.Discipline
                .Where(x => _employe.id == x.id_employe)
                .ToArray();


            dataDiscipline.ItemsSource = disp.Select(x => new DisciplineViwe
            {
                id = x.id,
                code = x.code,
                name = x.name,
                hours = $"{x.size}ч.",
                kafedra = x.Specialization.Department.name
            });
        }

        private void SerchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var disp = _connection.Discipline
                .Where(x => _employe.id == x.id_employe)
                .Where(x => x.name.ToUpper().Contains(SerchBox.Text.ToUpper()))
                .ToArray();


            dataDiscipline.ItemsSource = disp.Select(x => new DisciplineViwe
            {
                id = x.id,
                code = x.code,
                name = x.name,
                hours = $"{x.size}ч.",
                kafedra = x.Specialization.Department.name
            });
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinesAddEditPage(null, _employe));
        }

        private void DeletedButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_discipline != null)
                {
                    using (var context = new DataBaseContext())
                    {
                        var disciplineToDelete = context.Discipline.Find(_discipline.id);
                        if (disciplineToDelete != null)
                        {
                            context.Discipline.Remove(disciplineToDelete);
                            context.SaveChanges();
                            Load_Student(sender, e);
                            MessageBox.Show("Дисциплина удалена.", "Удача", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Дисциплина не найдена.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выберите дисциплину.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении дисциплины: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if(_discipline is null)
            {
                MessageBox.Show("Выберите дисциплину!");
            }
            else
            {
                NavigationService.Navigate(new DisciplinesAddEditPage(_discipline, _employe));
            }
        }

        private void dataDiscipline_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            var rowView = dataGrid.SelectedItem as DisciplineViwe;
            if (rowView is null)
                return;
            _discipline = _connection.Discipline.FirstOrDefault(x => x.id == rowView.id);
        }

        private class DisciplineViwe
        {
            public int id { get; set; }
            public string name { get; set; }
            public string code { get; set; }
            public string hours { get; set; }
            public string kafedra { get; set; }
            public string groupName { get; set; }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
        private void SortAscButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedDisciplines = _connection.Discipline
                .Where(x => _employe.id == x.id_employe)
                .OrderBy(x => x.size)
                .ToArray();

            dataDiscipline.ItemsSource = sortedDisciplines.Select(x => new DisciplineViwe
            {
                id = x.id,
                code = x.code,
                name = x.name,
                hours = $"{x.size}ч.",
                kafedra = x.Specialization.Department.name
            });
        }

        private void SortDescButton_Click(object sender, RoutedEventArgs e)
        {
            var sortedDisciplines = _connection.Discipline
                .Where(x => _employe.id == x.id_employe)
                .OrderByDescending(x => x.size)
                .ToArray();

            dataDiscipline.ItemsSource = sortedDisciplines.Select(x => new DisciplineViwe
            {
                id = x.id,
                code = x.code,
                name = x.name,
                hours = $"{x.size}ч.",
                kafedra = x.Specialization.Department.name
            });
        }
    }
}

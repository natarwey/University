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
            if(_employe is null)
            {
                MessageBox.Show("Не выбрана дисциплина!");
            }
            else
            {
                var exams = _connection.Exam.Where(x => x.id_distcipline == _employe.id).ToList();

                //_connection.ExamResult.RemoveRange(exams.SelectMany(x => x.ExamResult));
                //_connection.Exam.RemoveRange(exams);
                _connection.Employe.Remove(_employe);
                MessageBox.Show("Удаление выполнено!");

                // TODO а может что-то другое
                Load_Student(null, null);
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
    }
}

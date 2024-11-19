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
using System.Windows.Navigation;
using System.Windows.Shapes;
using University.Data;

namespace University.Pages
{
    /// <summary>
    /// Логика взаимодействия для DisciplinesByStudentPage.xaml
    /// </summary>
    public partial class DisciplinesByStudentPage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private readonly Student _student;
        private readonly Group _group;

        public DisciplinesByStudentPage(Student student)
        {
            InitializeComponent();
            _student = student;
            _group = student.Group;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            var disp = _connection.Discipline.Where(x => x.id_specialization == _group.id_specialization).Select(x => new
            {
                Code = x.code,
                Name = x.name,
                Hours = $"{x.size}ч.",
                Kafedra = x.Specialization.Department.name
            });
            dataDiscipline.ItemsSource = disp.ToList();
        }

        private void SerchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var disp = _connection.Discipline.Where(x => x.id_specialization == _group.id_specialization).Where(x => x.name.ToUpper().Contains(SerchBox.Text.ToUpper())).Select(x => new
            {
                Code = x.code,
                Name = x.name,
                Hours = $"{x.size}ч.",
                Kafedra = x.Specialization.Department.name
            });
            dataDiscipline.ItemsSource = disp.ToList();
        }
         
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}

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
    /// Логика взаимодействия для MainPageByStudent.xaml
    /// </summary>
    public partial class MainPageByStudent : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private readonly Student _student;

        public MainPageByStudent(Student student, People people)
        {
            _student = student;
            InitializeComponent();
            lableNameStudent.Content = $"{people.Fio}";
        }

        private void btnDisciplines_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinesByStudentPage(_student));
        }

        private void btnExam_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinesByStudentPage(_student));
        }
    }
}

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
    /// Логика взаимодействия для MainPageByEmploye.xaml
    /// </summary>
    public partial class MainPageByEmploye : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private readonly Employe _employer;

        public MainPageByEmploye(Employe employer, People people)
        {
            _employer = employer;
            InitializeComponent();
            lableNameStudent.Content = $"{people.Fio}";
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeeByEmployePage(_employer));
        }

        private void btnDepartments_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepartamentPage(_employer));
        }

        private void btnDisciplines_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinesByEmployePage(_employer));
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}

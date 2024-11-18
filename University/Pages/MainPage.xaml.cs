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
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private Employee _currentEmployee = null;
        public MainPage(Employee employee)
        {
            _currentEmployee = employee;
            InitializeComponent();
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployeesPage());
        }

        private void btnDepartments_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DepartmentsPage());
        }

        private void btnDisciplines_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new DisciplinesPage());
        }
    }
}

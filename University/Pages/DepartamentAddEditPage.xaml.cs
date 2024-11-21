using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для DisciplinesAddEditPage.xaml
    /// </summary>
    public partial class DepartamentAddEditPage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private Department _department;
        private readonly Employe _employe;

        public DepartamentAddEditPage(Department department, Employe employe)
        {
            InitializeComponent();
            _department = department;
            _employe = employe;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            if (_department != null)
            {
                TextBoxName.Text = "Редактирование кафедры";
                IdBox.Text = _department.id.ToString();
                NameBox.Text = _department.name;
            }
            else
            {
                TextBoxName.Text = "Создание кафедры";
            }
        }

        private void IdBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9,]", "");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("Пожалуйста, введите название кафедры.");
                return;
            }
            else
            {
                if (_department is null)
                {
                    // когда создание
                    _department = new Department();
                    _department.id = int.TryParse(IdBox.Text, out var value) ? value : 0;
                    _department.name = NameBox.Text;
                    //_department.id = _employe.id_department;

                    using (var context = new DataBaseContext())
                    {
                        _connection.Employe.Add(_employe);
                        _connection.SaveChanges();
                    }

                    MessageBox.Show("Кафедра сохранена!");
                    NavigationService.GoBack();
                    //NavigationService.RemoveBackEntry(); 
                }
                else
                {
                    // когда редактирование
                    _department = new Department();
                    _department.id = int.TryParse(IdBox.Text, out var value) ? value : 0;
                    _department.name = NameBox.Text;

                    using (var context = new DataBaseContext())
                    {
                        _connection.SaveChanges();
                    }

                    MessageBox.Show("Кафедра сохранена!");
                    NavigationService.GoBack();
                    //NavigationService.RemoveBackEntry();
                    
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }
    }
}

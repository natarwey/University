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
    public partial class DisciplinesAddEditPage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private Discipline _discipline;
        private readonly Employe _employe;

        public DisciplinesAddEditPage(Discipline discipline, Employe employe)
        {
            InitializeComponent();
            _discipline = discipline;
            _employe = employe;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            var spec = _connection.Specialization.Where(x => x.id_department == _employe.id_department).ToList();
            SpecializBox.ItemsSource = spec;
            SpecializBox.DisplayMemberPath = "Name";
            if (_discipline != null)
            {
                TextBoxName.Text = "Редактирование дисциплины";
                NameBox.Text = _discipline.name;
                SizeBox.Text = _discipline.size.ToString();
                CodeBox.Text = _discipline.code;
                var index = spec.Select(x => x.id).ToList().IndexOf(_discipline.Specialization.id);
                SpecializBox.SelectedIndex = index;
            }
            else
            {
                TextBoxName.Text = "Создание дисциплины";
            }
        }

        private void SizeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ((TextBox)sender).Text = Regex.Replace(((TextBox)sender).Text, "[^0-9,]", "");
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SpecializBox.SelectedIndex == -1)
            {
                MessageBox.Show("Пожалуйста, выберите специальность для дисциплины.");
                return;
            }
            else
            {
                if (_discipline is null)
                {
                    // когда создание
                    _discipline = new Discipline();
                    _discipline.code = CodeBox.Text;
                    _discipline.name = NameBox.Text;
                    _discipline.size = int.TryParse(SizeBox.Text, out var value) ? value : 0;
                    _discipline.id_specialization = (SpecializBox.SelectedItem as Specialization).id;
                    _discipline.id_employe = _employe.id;

                    _connection.Discipline.Add(_discipline);
                    _connection.SaveChanges();

                    // строчка для возвращения назад
                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();
                    MessageBox.Show("Дисциплина сохранена!");
                }
                else
                {
                    // когда редактирование
                    _discipline.code = CodeBox.Text;
                    _discipline.name = NameBox.Text;
                    _discipline.size = int.TryParse(SizeBox.Text, out var value) ? value : 0;
                    _discipline.id_specialization = (SpecializBox.SelectedItem as Specialization).id;

                    _connection.SaveChanges();

                    NavigationService.GoBack();
                    NavigationService.RemoveBackEntry();
                    MessageBox.Show("Дисциплина сохранена!");
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

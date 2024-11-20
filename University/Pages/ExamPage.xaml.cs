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
    /// Логика взаимодействия для DisciplinesPage.xaml
    /// </summary>
    public partial class ExamPage : Page
    {
        private static DataBaseContext _connection = new DataBaseContext();
        private object _exam;
        private readonly Student _student;
        private readonly Group _group;

        public ExamPage(Student student)
        {
            InitializeComponent();
            _student = student;
            _group = student.Group;
        }

        private void Load_Student(object sender, RoutedEventArgs e)
        {
            var disp = _group.Exam.Select(x => new
            {
                Code = x.code,
                Name = x.Discipline.name,
                Date = x.date,
                Audit = x.ayditory,
                Prepod = x.Employe.People.Fio,
                Result = x.ExamResult.FirstOrDefault(y=>y.id_student == _student.id)?.result
            });
            dataExam.ItemsSource = disp.ToList();
        }

        private void SerchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var disp = _group.Exam.Where(x => x.Discipline.name.ToUpper().Contains(SerchBox.Text.ToUpper())).Select(x => new
            {
                Code = x.code,
                Name = x.Discipline.name,
                Date = x.date,
                Audit = x.ayditory,
                Prepod = x.Employe.People.Fio,
                Result = x.ExamResult.FirstOrDefault(y => y.id_student == _student.id)?.result
            });
            dataExam.ItemsSource = disp.ToList();
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            NavigationService.RemoveBackEntry();
        }

        private void dataExam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            var rowView = dataGrid.SelectedItem as dynamic;
            if (rowView is null)
                return;
            string examCode = rowView.Code;
            _exam = _group.Exam.FirstOrDefault(x => x.code.ToString() == examCode);
        }
    }
}

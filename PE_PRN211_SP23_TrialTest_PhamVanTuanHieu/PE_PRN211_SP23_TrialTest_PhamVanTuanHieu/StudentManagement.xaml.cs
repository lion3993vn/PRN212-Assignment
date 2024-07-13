using Microsoft.EntityFrameworkCore.Query;
using Repository.Entities;
using Repository.Repositories;
using Service.Services;
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
using System.Windows.Shapes;

namespace PE_PRN211_SP23_TrialTest_PhamVanTuanHieu
{
    /// <summary>
    /// Interaction logic for StudentManagement.xaml
    /// </summary>
    public partial class StudentManagement : Window
    {
        private StudentModel _selected;
        private StudentService _studentService;
        public StudentManagement()
        {
            InitializeComponent();
        }

        private void StudentManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            _studentService = new StudentService();
            var list = _studentService.getAllStudent();
            dgvStudent.ItemsSource = list;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            _studentService = new StudentService();
            int fromYear = 0, toYear = 0;
            if (string.IsNullOrEmpty(txtFromYear.Text) || string.IsNullOrEmpty(txtToYear.Text))
            {
                MessageBox.Show("Please input both 2 field", "Error", MessageBoxButton.OK);
                return;
            }
            else
            {
                try
                {
                    fromYear = int.Parse(txtFromYear.Text);
                    toYear = int.Parse(txtToYear.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Please input number", "Error", MessageBoxButton.OK);
                    return;
                }
            }
            
            if(fromYear > toYear)
            {
                int temp = fromYear;
                fromYear = toYear;
                toYear = temp;
            }
            var list = _studentService.searchStudent(fromYear, toYear);
            dgvStudent.ItemsSource = list;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dgvStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dgvStudent.SelectedItem != null)
            {
                _selected = (StudentModel)dgvStudent.SelectedItem;
            }
            else
            {
                _selected = null;
            }
        }
    }
}

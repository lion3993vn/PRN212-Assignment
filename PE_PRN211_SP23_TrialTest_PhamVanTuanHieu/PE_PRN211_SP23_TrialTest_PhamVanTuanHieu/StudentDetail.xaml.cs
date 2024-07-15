using Repository.Entities;
using Repository.Model;
using Repository.Repositories;
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
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PE_PRN211_SP23_TrialTest_PhamVanTuanHieu
{
    /// <summary>
    /// Interaction logic for StudentDetail.xaml
    /// </summary>
    public partial class StudentDetail : Window
    {
        public StudentModel selected = null;
        private StudentRepository _studentRepository;
        private StudentGroupRepository _studentGroupRepository;

        public StudentDetail()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool checkCapital(string input)
        {
            // Kiểm tra chuỗi rỗng hoặc null
            if (string.IsNullOrEmpty(input))
            {
                return false;
            }

            // Tách chuỗi thành các từ
            string[] words = input.Split(' ');

            // Duyệt qua từng từ
            foreach (string word in words)
            {
                // Kiểm tra từ có bắt đầu bằng chữ cái viết hoa hay không
                if (!char.IsUpper(word[0]))
                {
                    return false;
                }

                // Kiểm tra từ có chứa ký tự đặc biệt hay không
                if (!Regex.IsMatch(word, @"^[a-zA-Z]+$"))
                {
                    return false;
                }
            }
            return true;
        }

        private bool Validate(string name, string email, DateTime date)
        {
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email is required", "Error", MessageBoxButton.OK);
                return false;
            }
            else if (!email.Contains("@"))
            {
                MessageBox.Show("Email must have @", "Error", MessageBoxButton.OK);
                return false;
            }
            if (name.Length < 5 || name.Length > 40)
            {
                MessageBox.Show("Length of Student fullname is in the range of 5 – 40 characters", "Error", MessageBoxButton.OK);
                return false;
            }
            else if (!checkCapital(name))
            {
                MessageBox.Show("Each word in name must begin with the capital letter and special characters are not allowed", "Error", MessageBoxButton.OK);
                return false;
            }
            DateTime valid = new DateTime(2024, 1, 1);
            if (date == null)
            {
                MessageBox.Show("DateOfBirth is required", "Error", MessageBoxButton.OK);
                return false;
            }
            else if (date >= valid)
            {
                MessageBox.Show("DateOfBirth must be before 1/1/2005", "Error", MessageBoxButton.OK);
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _studentRepository = new();
                if (selected == null)
                {
                    string name = txtFullname.Text;
                    string email = txtEmail.Text;
                    DateTime date = dtpBirthday.SelectedDate.Value;
                    if(!Validate(name, email, date))
                    {
                        return;
                    }
                    int groupid = ((StudentGroup)cbGroup.SelectedItem).Id;
                    Student student = new Student()
                    {
                        GroupId = groupid,
                        DateOfBirth = date,
                        Email = email,
                        FullName = name,
                    };

                    _studentRepository.addNewStudent(student);
                    this.Close();
                }
                else
                {
                    string name = txtFullname.Text;
                    string email = txtEmail.Text;
                    DateTime date = dtpBirthday.SelectedDate.Value;
                    int groupid = ((StudentGroup)cbGroup.SelectedItem).Id;
                    if (!Validate(name, email, date))
                    {
                        return;
                    }
                    else
                    {
                        Student student = new Student()
                        {
                            Id = selected.Id,
                            GroupId = groupid,
                            DateOfBirth = date,
                            Email = email,
                            FullName = name,
                        };
                        _studentRepository.updateStudent(student);
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error", "Error", MessageBoxButton.OK);
            }
        }

        private void cbGroup_Loaded(object sender, RoutedEventArgs e)
        {
            _studentGroupRepository = new();
            cbGroup.ItemsSource = _studentGroupRepository.getAllGroup();
            cbGroup.DisplayMemberPath = "GroupName";
            cbGroup.SelectedValuePath = "Id";
            if (selected == null)
            {
                cbGroup.SelectedIndex = 0;
            }
            else
            {
                cbGroup.SelectedValue = (int)selected.GroupId;
            }
        }

        private void DetailForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (selected != null)
            {
                txtId.Text = selected.Id.ToString();
                txtEmail.Text = selected.Email;
                txtFullname.Text = selected.FullName;
                dtpBirthday.SelectedDate = selected.DateOfBirth;
            }
        }
    }
}

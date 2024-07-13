﻿using Repository.Entities;
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

namespace PE_PRN211_SP23_TrialTest_PhamVanTuanHieu
{
    /// <summary>
    /// Interaction logic for StudentDetail.xaml
    /// </summary>
    public partial class StudentDetail : Window
    {
        public StudentModel _selected = null;
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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selected == null)
                {
                    _studentRepository = new();
                    string name = txtFullname.Text;
                    string email = txtEmail.Text;
                    if(string.IsNullOrEmpty(email))
                    {
                        MessageBox.Show("Email is required", "Error", MessageBoxButton.OK);
                        return;
                    }
                    if (name.Length < 5 || name.Length > 40)
                    {
                        MessageBox.Show("Length of Student fullname is in the range of 5 – 40 characters", "Error", MessageBoxButton.OK);
                        return;
                    }
                    else if (!checkCapital(name))
                    {
                        MessageBox.Show("Each word in name must begin with the capital letter and special characters are not allowed", "Error", MessageBoxButton.OK);
                        return;
                    }
                    DateTime date = dtpBirthday.SelectedDate.Value;
                    DateTime valid = new DateTime(2024, 1, 1);
                    if(date == null)
                    {
                        MessageBox.Show("DateOfBirth is required", "Error", MessageBoxButton.OK);
                        return;
                    }
                    else if (date >= valid)
                    {
                        MessageBox.Show("DateOfBirth must be before 1/1/2005", "Error", MessageBoxButton.OK);
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
            if (_selected == null)
            {
                cbGroup.SelectedIndex = 0;
            }
            else
            {
                cbGroup.SelectedValue = (int)_selected.GroupId;
            }
        }

        private void DetailForm_Loaded(object sender, RoutedEventArgs e)
        {
            if (_selected != null)
            {
                txtId.Text = _selected.Id.ToString();
                txtEmail.Text = _selected.Email;
                txtFullname.Text = _selected.FullName;
                dtpBirthday.SelectedDate = _selected.DateOfBirth;
            }
        }
    }
}

﻿using Microsoft.EntityFrameworkCore.Query;
using Repository.Entities;
using Repository.Model;
using Repository.Repositories;
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
        private StudentRepository _studentRepository;
        private StudentGroupRepository _studentGroupRepository;
        private StudentModel selected = null;
        public StudentManagement()
        {
            InitializeComponent();
        }

        private void loadGrid()
        {
            _studentRepository = new();
            _studentGroupRepository = new();
            var list = _studentRepository.getAllStudent();
            var model = list.Select(x => new StudentModel()
            {
                Id = x.Id,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                FullName = x.FullName,
                GroupId = x.GroupId,
                GroupName = x.Group.GroupName,
            }).ToList();
            dgvStudent.ItemsSource = model;
        }

        private void StudentManagementForm_Loaded(object sender, RoutedEventArgs e)
        {
            loadGrid();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            _studentRepository = new();
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

            if (fromYear > toYear)
            {
                MessageBox.Show("From-year cannot bigger than To-year", "Error", MessageBoxButton.OK);
                return;
            }
            var list = _studentRepository.searchStudent(fromYear, toYear);
            var model = list.Select(x => new StudentModel()
            {
                Id = x.Id,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth,
                FullName = x.FullName,
                GroupId = x.GroupId,
                GroupName = x.Group.GroupName,
            }).ToList();
            dgvStudent.ItemsSource = model;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void dgvStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgvStudent.SelectedItem != null)
            {
                selected = (StudentModel)dgvStudent.SelectedItem;
            }
            else
            {
                selected = null;
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            StudentDetail form = new StudentDetail();
            form.selected = null;
            form.ShowDialog();
            loadGrid();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please choose the student you want to delete", "Error", MessageBoxButton.OK);
            }
            else
            {
                var result = MessageBox.Show("Are you sure to delete this student", "Confirm", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    _studentRepository = new();
                    _studentRepository.deleteStudent(new Student()
                    {
                        Id = selected.Id,
                        Email = selected.Email,
                        DateOfBirth = selected.DateOfBirth,
                        FullName = selected.FullName,
                        GroupId = selected.GroupId,
                    });

                    selected = null;
                    loadGrid();
                }
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(selected == null)
            {
                MessageBox.Show("Please choose an item to update", "Error", MessageBoxButton.OK);
            }
            else
            {
                StudentDetail form = new StudentDetail();
                form.selected = selected;
                form.ShowDialog();
                loadGrid();
            }
        }
    }
}

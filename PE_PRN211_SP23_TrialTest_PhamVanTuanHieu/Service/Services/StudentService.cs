using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StudentService
    {
        private StudentRepository _studentRepository;
        private StudentGroupRepository _studentGroupRepository;

        public List<StudentModel> getAllStudent()
        {
            _studentRepository = new();
            var list = _studentRepository.getAllStudent();
            return list.Select(x => new StudentModel()
            {
                Id = x.Id,
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                FullName = x.FullName,
                GroupId = x.GroupId,
                GroupName = x.Group.GroupName,
            }).ToList();

        }

        public List<StudentModel> searchStudent(int fromYear, int toYear)
        {
            _studentGroupRepository = new();
            _studentRepository = new();
            var list = _studentRepository.searchStudent(fromYear, toYear);
            return list.Select(x => new StudentModel()
            {
                Id = x.Id,
                DateOfBirth = x.DateOfBirth,
                Email = x.Email,
                FullName = x.FullName,
                GroupId = x.GroupId,
                GroupName = x.Group.GroupName,
            }).ToList();
        }

        public void addStudent(Student student)
        {
            _studentRepository = new();
            _studentRepository.addNewStudent(student);
        }

        public Student getStudentById(int id)
        {
            _studentRepository = new();
            return _studentRepository.getStudentById(id);
        }
    }
}

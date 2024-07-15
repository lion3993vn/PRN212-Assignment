using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentRepository
    {
        private Prn231Su23StudentGroupDbContext _context;

        public List<Student> getAllStudent()
        {
            _context = new();
            return _context.Students.Include(x => x.Group).ToList();
        }

        public List<Student> searchStudent(int fromYear, int toYear)
        {
            _context = new();
            var list = _context.Students.Where(x => x.DateOfBirth.Value.Year >= fromYear && x.DateOfBirth.Value.Year <= toYear).Include(x => x.Group).ToList();
            return list;
        }

        public void addNewStudent(Student student)
        {
            _context = new();
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        public Student getStudentById(int id)
        {
            _context = new();
            return _context.Students.FirstOrDefault(x => x.Id == id);
        }

        public void deleteStudent(Student student)
        {
            _context = new();
            _context.Remove(student);
            _context.SaveChanges();
        }

        public void updateStudent(Student student)
        {
            _context = new();
            _context.Students.Update(student);
            _context.SaveChanges();
        }
    }
}

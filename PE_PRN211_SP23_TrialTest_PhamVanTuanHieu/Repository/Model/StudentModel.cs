using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class StudentModel
    {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? FullName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? GroupId { get; set; }

        public string? GroupName { get; set; }

    }
}

using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class StudentGroupRepository
    {
        private Prn231Su23StudentGroupDbContext _context;

        public string getGroupNameById(int id)
        {
            _context = new();
            var group = _context.StudentGroups.Where(x => x.Id == id).FirstOrDefault();
            return group.GroupName;
        }
    }
}

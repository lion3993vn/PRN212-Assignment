using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class StudentGroupService
    {
        private StudentGroupRepository _studentGroupRepository;

        public List<StudentGroup> getAllGroup()
        {
            _studentGroupRepository = new StudentGroupRepository();
            return _studentGroupRepository.getAllGroup();
        }
    }
}

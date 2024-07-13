using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserRoleService
    {
        private UserRoleRepository _repository;

        public UserRole? login(string username, string password)
        {
            _repository = new UserRoleRepository();
            return _repository.Login(username, password);
        }
    }
}

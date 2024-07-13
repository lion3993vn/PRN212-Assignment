using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRoleRepository
    {
        private Prn231Su23StudentGroupDbContext _context;

        public UserRole? Login(string username, string password)
        {
            _context = new Prn231Su23StudentGroupDbContext();
            return _context.UserRoles.Where(x => x.Username.Equals(username) || x.Passphrase.Equals(password)).FirstOrDefault();
        }
    }
}

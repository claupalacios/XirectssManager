using System.Collections.Generic;
using System.Linq;
using UserMS.Models;
using UserMS.Repositories.Interfaces;

namespace UserMS.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext _context;

        public UserRepository(UserDBContext context)
        {
            _context = context;
        }
        public List<User> GetAllActiveUsers()
        {
            return _context.Users.Where(x => x.Active).ToList();
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return _context.Users.Find(user.Id);
        }

        public bool UpdateUserState(int userId, bool active)
        {
            var userToUpdate = _context.Users.Find(userId);
            if (userToUpdate != null)
            {
                _context.Users.Attach(userToUpdate);
                userToUpdate.Active = active;
                _context.Entry(userToUpdate).Property(x => x.Active).IsModified = true;
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}

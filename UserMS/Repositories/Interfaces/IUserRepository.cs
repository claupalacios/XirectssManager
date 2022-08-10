using System.Collections.Generic;
using UserMS.Models;

namespace UserMS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetAllActiveUsers();
        public User AddUser(User user);
        public bool UpdateUserState(int userId, bool active);
    }
}

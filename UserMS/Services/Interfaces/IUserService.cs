using Common.Models;
using System.Collections.Generic;
using UserMS.Dtos;
using UserMS.Models;

namespace UserMS.Services.Interfaces
{
    public interface IUserService
    {
        Response<List<User>> GetAllActiveUsers();
        Response<object> AddUser(UserDto user);
        Response<object> UpdateUserState(int userId, bool active);
    }
}

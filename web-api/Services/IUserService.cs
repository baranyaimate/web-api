using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IUserService
{
    IEnumerable<User> GetAll();

    User GetUserById(int id);

    User UpdateUser(int id, UserDto userDto);

    User SaveUser(UserDto userDto);

    void DeleteUser(int id);
}
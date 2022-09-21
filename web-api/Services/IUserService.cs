using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IUserService
{
    ActionResult<IEnumerable<User>> GetAll();
    
    ActionResult<User> GetUserById(int id);

    ActionResult<User> UpdateUser(int id, UserDto userDto);
    
    ActionResult<User> SaveUser(UserDto userDto);

    void DeleteUser(int id);
    
}
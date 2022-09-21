using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;
using web_api.Services;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    // GET: api/user
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetAll()
    {
        return _userService.GetAll();
    }
    
    // GET: api/user/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        return _userService.GetUserById(id);
    }
    
    // PUT: api/user/{id}
    [HttpPut("{id}")]
    public ActionResult<User> UpdateUser(int id, UserDto userDto)
    {
        try
        {
            return _userService.UpdateUser(id, userDto);
        }
        catch
        {
            return NotFound();
        }
    }
    
    // POST: api/user
    [HttpPost]
    public ActionResult<User> SaveUser(UserDto userDto)
    {
        return _userService.SaveUser(userDto);
    }
    
    // DELETE: api/user/{id}
    [HttpDelete("{id}")]
    public void DeleteUser(int id)
    {
        _userService.DeleteUser(id);
    }
    
}
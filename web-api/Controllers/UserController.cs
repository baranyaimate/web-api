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
        return _userService.GetAll().ToList();
    }

    // GET: api/user/{id}
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        try
        {
            return _userService.GetUserById(id);
        }
        catch
        {
            return BadRequest();
        }
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
            return BadRequest();
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
    public ActionResult DeleteUser(int id)
    {
        try
        {
            _userService.DeleteUser(id);
            return Ok("The user was deleted");
        }
        catch
        {
            return BadRequest();
        }
    }
}
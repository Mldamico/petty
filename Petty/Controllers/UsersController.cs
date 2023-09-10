using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petty.Data;
using Petty.Entities;
using Petty.Interfaces;

namespace Petty.Controllers;

public class UsersController : BaseController
{
    private readonly IUserRepository _userRepository;
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
    {
        return Ok(await _userRepository.GetUsersAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return user;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<AppUser>> GetUserByUsername(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username);
        return user;
    }
}
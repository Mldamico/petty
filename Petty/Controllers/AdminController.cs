using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petty.Entities;


namespace Petty.Controllers;

public class AdminController : BaseController
{
    private readonly UserManager<AppUser> _userManager;

    public AdminController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ActionResult> GetUserWithRoles()
    {
        var users = await _userManager.Users.OrderBy(u => u.UserName).Select(u => new
        {
            u.Id,
            Username = u.UserName,
            Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
        }).ToListAsync();

        return Ok(users);
    }
}
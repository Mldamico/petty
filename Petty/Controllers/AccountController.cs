using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petty.DTO;
using Petty.Entities;
using Petty.Interfaces;

namespace Petty.Controllers;

public class AccountController : BaseController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, IMapper mapper, ITokenService tokenService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username)) return BadRequest("Username is taken");

        var user = _mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);
        var roleResult = await _userManager.AddToRoleAsync(user, "Member");
        if (!roleResult.Succeeded) return BadRequest(roleResult.Errors);

        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = _userManager.Users.SingleOrDefault(x => x.UserName == loginDto.Username);

        if (user == null) return Unauthorized("Invalid credentials");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result) return Unauthorized("Invalid credentials");
        return new UserDto
        {
            Username = user.UserName,
            Token = await _tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string username)
    {
        return await _userManager.Users.AnyAsync(user => user.UserName == username.ToLower());
    }
}
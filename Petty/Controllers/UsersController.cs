using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Petty.Data;
using Petty.DTO;
using Petty.Entities;
using Petty.Interfaces;

namespace Petty.Controllers;

public class UsersController : BaseController
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UsersController(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync();
   
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetUserById(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
        return _mapper.Map<MemberDto>(user);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUserByUsername(string username)
    {
        return await _userRepository.GetMemberAsync(username);
        
    }
}
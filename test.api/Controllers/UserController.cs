using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test.app.DTOs;
using test.app.Service;
using test.domain.Entities;

namespace test.api.Controllers;

[Authorize]
[ApiController]
[Route("{tenant}/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Post(UserDTO userDTO)
    {
        return await _userService.Post(userDTO);
    }

}

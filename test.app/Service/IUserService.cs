
using Microsoft.AspNetCore.Mvc;
using test.app.DTOs;

namespace test.app.Service;

public interface IUserService
{
    Task<IActionResult> Post(UserDTO userDTO);

}

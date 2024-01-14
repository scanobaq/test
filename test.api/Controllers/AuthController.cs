
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using test.app.DTOs;
using test.app.Service;

namespace test.api.Controllers
{
    [ApiController]
    [Route("{tenant}/api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthController(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;

        }

        [HttpPost("Login")]
        public string Login(UserLoginDto userLoginDto)
        {
            return _jwtTokenGenerator.GenerateJwtToken(userLoginDto);
        }
    }
}
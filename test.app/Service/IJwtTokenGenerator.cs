
using System.IdentityModel.Tokens.Jwt;
using test.app.DTOs;

namespace test.app.Service;
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(UserLoginDto user);
}

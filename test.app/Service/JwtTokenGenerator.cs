using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using test.app.DTOs;
using test.domain.Entities;
namespace test.app.Service;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly SymmetricSecurityKey _key;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _config;

    public JwtTokenGenerator(IConfiguration config, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
        _signInManager = signInManager;
        _userManager = userManager;
        _config = config;
    }
    public string GenerateJwtToken(UserLoginDto user)
    {
        try
        {
            var userData = _userManager.FindByEmailAsync(user.UserName).Result;
            var result = _signInManager.CheckPasswordSignInAsync(userData, user.Password, false).Result;
            if (!result.Succeeded) throw new Exception("Invalid user or password");
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,userData.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userData.Email)

            };
            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["Tokens:Issuer"],
                audience: _config["Tokens:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }
        catch (System.Exception)
        {
            throw new Exception("Invalid user or password");
        }

    }
}

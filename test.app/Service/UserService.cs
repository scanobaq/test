
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test.app.DTOs;
using test.domain.Entities;

namespace test.app.Service;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly ICompanyService _companyService;
    private readonly IMapper _mapper;
    public UserService(UserManager<User> userManager, ICompanyService companyService, IMapper mapper)
    {
        _userManager = userManager;
        _companyService = companyService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Post(UserDTO userDTO)
    {
        try
        {
            var company = await _companyService.GetByIdAsync(userDTO.CompanyId) ?? throw new Exception("Company not found");
            userDTO.Company = company;
            var user = _mapper.Map<User>(userDTO);
            var createUserResult = await _userManager.CreateAsync(user, userDTO.Password);
            if (!createUserResult.Succeeded)
            {
                throw new Exception(createUserResult.Errors.ToString());
            }

            return new CreatedAtActionResult("Post", "User", new { id = user.Id }, user);

        }
        catch (System.Exception)
        {
            throw new Exception("Error creating user");
        }

    }


}

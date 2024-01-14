
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test.app.DTOs;
using test.app.Service;

namespace test.api.Controllers
{

    [ApiController]
    [Route("{tenant}/api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _serviceCompany;

        public CompanyController(ICompanyService serviceCompany)
        {
            _serviceCompany = serviceCompany;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var companies = await _serviceCompany.GetAllAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _serviceCompany.GetByIdAsync(id);
            return Ok(company);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyDto companyDto)
        {
            await _serviceCompany.AddAsync(companyDto);
            return CreatedAtAction("Post", new { id = companyDto.Id }, companyDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CompanyDto companyDto)
        {
            await _serviceCompany.UpdateAsync(companyDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceCompany.DeleteAsync(id);
            return NoContent();
        }

    }
}

using test.app.DTOs;
using test.domain.Entities;

namespace test.app.Service
{
    public interface ICompanyService
    {
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<Company> GetByIdAsync(int id);
        Task AddAsync(CompanyDto companyDto);
        Task UpdateAsync(CompanyDto companyDto);
        Task DeleteAsync(int id);

    }
}
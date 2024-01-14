using Microsoft.EntityFrameworkCore;
using test.domain.Entities;
using test.domain.Interfaces;
using test.infrastructure.Data;

namespace test.infrastructure.Repository;
public class CompanyRepository(DBContextCompany context) : ICompanyRepository
{
    private readonly DBContextCompany _context = context;

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _context.Companies.ToListAsync();
    }

    public async Task<Company> GetByIdAsync(int id)
    {
        return await _context.Companies.FindAsync(id);
    }

    public async Task<Company> CreateAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task UpdateAsync(Company company)
    {
        _context.Entry(company).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var companyToDelete = await _context.Companies.FindAsync(id);
        _context.Companies.Remove(companyToDelete);
        await _context.SaveChangesAsync();
    }
}

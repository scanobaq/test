using AutoMapper;
using test.app.DTOs;
using test.domain.Entities;
using test.domain.Interfaces;

namespace test.app.Service;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;
    private readonly IMapper _mapper;

    public CompanyService(ICompanyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CompanyDto>> GetAllAsync()
    {
        var company = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CompanyDto>>(company);

    }

    public async Task<Company> GetByIdAsync(int id)
    {
        var company = await _repository.GetByIdAsync(id);
        return company;
        //return _mapper.Map<CompanyDto>(company);
    }

    public async Task AddAsync(CompanyDto companyDto)
    {
        var company = _mapper.Map<Company>(companyDto);
        await _repository.CreateAsync(company);
    }

    public async Task UpdateAsync(CompanyDto companyDto)
    {
        var company = _mapper.Map<Company>(companyDto);
        await _repository.UpdateAsync(company);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }



}

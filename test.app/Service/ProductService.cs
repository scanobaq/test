using System.Data.Common;
using AutoMapper;
using test.app.DTOs;
using test.domain.Entities;
using test.domain.Interfaces;

namespace test.app.Service;
public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;


    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;

    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        try
        {
            var product = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(product);
        }
        catch (DbException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        try
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            return _mapper.Map<ProductDto>(product);
        }
        catch (DbException ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task AddAsync(ProductDto productDto)
    {
        try
        {
            var product = _mapper.Map<Product>(productDto);
            await _repository.AddAsync(product);
        }
        catch (DbException ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task UpdateAsync(ProductDto productDto)
    {
        try
        {
            var productToUpdate = await _repository.GetByIdAsync(productDto.Id) ?? throw new Exception("Product not found");
            var product = _mapper.Map<Product>(productDto);
            await _repository.UpdateAsync(product);

        }
        catch (DbException ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task DeleteAsync(int id)
    {
        try
        {
            await _repository.DeleteAsync(id);
        }
        catch (DbException ex)
        {
            throw new Exception(ex.Message);
        }
    }

}

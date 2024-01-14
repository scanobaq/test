using test.app.DTOs;

namespace test.app.Service;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto> GetByIdAsync(int id);
    Task AddAsync(ProductDto productDto);
    Task UpdateAsync(ProductDto productDto);
    Task DeleteAsync(int id);

}


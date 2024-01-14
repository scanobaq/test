
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using test.app.DTOs;
using test.app.Service;

namespace test.api.Controllers;

[Authorize]
[ApiController]
[Route("{tenant}/api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _productService.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post(ProductDto productDto)
    {
        await _productService.AddAsync(productDto);
        return CreatedAtAction("Post", new { id = productDto.Id }, productDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, ProductDto productDto)
    {
        await _productService.UpdateAsync(productDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _productService.DeleteAsync(id);
        return NoContent();
    }


}

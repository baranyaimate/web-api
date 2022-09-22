using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;
using web_api.Services;

namespace web_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/product
    [HttpGet]
    public IEnumerable<Product> GetAll()
    {
        return _productService.GetAll();
    }

    // GET: api/product/{id}
    [HttpGet("{id}")]
    public ActionResult<Product> GetProduct(int id)
    {
        try
        {
            return _productService.GetProductById(id);
        }
        catch
        {
            return BadRequest();
        }
    }

    // PUT: api/product/{id}
    [HttpPut("{id}")]
    public ActionResult<Product> UpdateProduct(int id, ProductDto productDto)
    {
        try
        {
            return _productService.UpdateProduct(id, productDto);
        }
        catch
        {
            return NotFound();
        }
    }

    // POST: api/product
    [HttpPost]
    public ActionResult<Product> SaveProduct(ProductDto productDto)
    {
        return _productService.SaveProduct(productDto);
    }

    // DELETE: api/product/{id}
    [HttpDelete("{id}")]
    public void DeleteProduct(int id)
    {
        _productService.DeleteProduct(id);
    }
}
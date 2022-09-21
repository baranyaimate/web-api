using Microsoft.AspNetCore.Mvc;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services;

public interface IProductService
{
    ActionResult<IEnumerable<Product>> GetAll();
    
    ActionResult<Product> GetProductById(int id);

    ActionResult<Product> UpdateProduct(int id, ProductDto productDto);
    
    ActionResult<Product> SaveProduct(ProductDto productDto);

    void DeleteProduct(int id);
}
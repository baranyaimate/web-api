using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class ProductServiceImpl : IProductService
{
    public ActionResult<IEnumerable<Product>> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<Product>().ToList();
    }
    
    public ActionResult<Product> GetProductById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Product>().Single(x => x.Id == id);
    }

    public void DeleteProduct(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Product>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public ActionResult<Product> UpdateProduct(int id, ProductDto productDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var oldProduct = session.Query<Product>().SingleOrDefault(x => x.Id == id);

        if (oldProduct == null)
        {
            throw new Exception("Product not found");
        }

        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            CreatedAt = oldProduct.CreatedAt,
            UpdatedAt = DateTime.Now
        };
        
        session.Update(product);

        return product;
    }

    public ActionResult<Product> SaveProduct(ProductDto productDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = new Product
        {
            Name = productDto.Name,
            Price = productDto.Price,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };

        session.Save(product);
        
        return product;
    }
}
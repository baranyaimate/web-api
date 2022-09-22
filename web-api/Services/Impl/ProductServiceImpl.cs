using Mapster;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class ProductServiceImpl : IProductService
{
    public IEnumerable<Product> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Product>().ToList();
    }

    public Product GetProductById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = session.Query<Product>().SingleOrDefault(x => x.Id == id);

        if (product == null) throw new BadHttpRequestException("Product not found");

        return product;
    }

    public void DeleteProduct(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        session.Query<Product>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public Product UpdateProduct(int id, ProductDto productDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = productDto.Adapt<Product>();

        using var transaction = session.BeginTransaction();
        
        session.Merge(product);
        transaction.Commit();

        return product;
    }

    public Product SaveProduct(ProductDto productDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = productDto.Adapt<Product>();

        session.Save(product);

        return product;
    }

    public List<Product> GetProductsByIds(int[] ids)
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return session.Query<Product>().Where(p => ids.Any(x => x == p.Id)).ToList();
    }
}
using FluentNHibernate.Conventions;
using MapsterMapper;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class ProductServiceImpl : IProductService
{
    private readonly IMapper _mapper;
    
    public ProductServiceImpl(IMapper mapper)
    {
        _mapper = mapper;
    }

    public IEnumerable<Product> GetAll()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Product>().ToList();
    }

    public Product GetProductById(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = session.Query<Product>().SingleOrDefault(x => x.Id == id);

        if (product is null) throw new BadHttpRequestException($"Product({id}) not found");

        return product;
    }

    public void DeleteProduct(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = GetProductById(id);

        if (product is null) throw new Exception($"Product({id}) not found");

        session.Query<Product>()
            .Where(x => x.Id == id)
            .Delete();
    }

    public Product UpdateProduct(int id, ProductDto productDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = _mapper.Map<Product>(productDto);
        product.Id = id;

        using var transaction = session.BeginTransaction();

        session.Update(product);
        transaction.Commit();

        return product;
    }

    public Product SaveProduct(ProductDto productDto)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var product = _mapper.Map<Product>(productDto);

        session.Save(product);

        return product;
    }

    public List<Product> GetProductsByIds(int[] ids)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        // TODO: fix this
        //return session.Query<Product>().Where(p => ids.Any(x => x == p.Id)).ToList();

        var products = new List<Product>();

        foreach (var id in ids)
        {
            var product = GetProductById(id);
            
            products.Add(product);
        }

        return products;
    }

    public IEnumerable<Product> GetProductsByUserId(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        /*
        var sql = $@"
             select p.* from products p
	            left join ordersHasProducts ohp on ohp.Product_id = p.Id
	            left join orders o on o.Id = ohp.Order_id
	            left join users u on u.Id = o.User_id
             where u.Id = {id}
        ";
        
        var products = session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBean<Product>()).List<Product>();
        
        if (products.IsEmpty()) throw new Exception("Product not found");
        
        return products;
        */
        
        var products = (from p in session.Query<Product>()
            join ohp in session.Query<OrderHasProduct>() on p.Id equals ohp.Product.Id
            join o in session.Query<Order>() on o.Id equals ohp.Order.Id
            join u in session.Query<User>() on u.Id equals o.UserId
            where u.Id == id);

        if (products is null) throw new Exception("Product not found");
        
        return products;
    }
    
    public bool IsEmpty()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Product>().IsEmpty();
    }
}
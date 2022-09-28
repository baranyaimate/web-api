using FluentNHibernate.Conventions;
using MapsterMapper;
using NHibernate.Linq;
using web_api.Models;
using web_api.Models.DTO;

namespace web_api.Services.Impl;

public class ProductServiceImpl : IProductService
{
    private readonly IMapper _mapper;

    private readonly IOrderService _orderService;

    private readonly IUserService _userService;
    
    public ProductServiceImpl(IMapper mapper, IOrderService orderService, IUserService userService)
    {
        _mapper = mapper;
        _orderService = orderService;
        _userService = userService;
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

        if (_userService.GetUserById(id).Equals(null)) throw new Exception($"User({id}) not found");
        
        var products = session.Query<Product>()
            .SelectMany(product => product.Orders.Where(order => order.User.Id == id).Select(order => product))
            .ToList();

        if (products.IsEmpty()) throw new Exception("Products not found");
        
        return products;
    }

    public int GetCountByProductId(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        if (GetProductById(id).Equals(null)) throw new Exception($"Product({id}) not found");
        
        return session.Query<Product>()
            .SelectMany(product => product.Orders.SelectMany(order => order.Products.Where(p => p.Id == id)))
            .Count();
    }

    public OrderSummaryDto GetOrderSummeryByUserId(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();

        var orders = _orderService.GetOrdersByUserId(id).ToList();
        
        return new OrderSummaryDto
        {
            Orders = orders,
            TotalPrice = orders.Select(order => order.Products.Sum(p => p.Price)).Sum(),
            TotalOrders = orders.Count,
            AverageProductPrice = orders.SelectMany(order => order.Products).Average(p => p.Price)
        };
    }
    
    public IEnumerable<ProductSummaryDto> GetProductSummary(int id)
    {
        using var session = FluentNHibernateHelper.OpenSession();
        
        return _orderService.GetOrdersByProductId(id)
            .SelectMany(order => order.Products)
            .Distinct()
            .Select(product => new ProductSummaryDto
            {
                Product = product,
                TotalSold = product.Orders.Count
            });
    }

    public int GetTotalIncome()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Order>()
            .SelectMany(order => order.Products)
            .Sum(product => product.Price);
    }

    public bool IsEmpty()
    {
        using var session = FluentNHibernateHelper.OpenSession();

        return session.Query<Product>().IsEmpty();
    }
}
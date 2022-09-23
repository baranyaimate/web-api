namespace web_api.Models;

public class Order
{
    public virtual int Id { get; set; }
    public virtual User User { get; set; }
    public virtual IList<Product> Products { get; set; }

    public Order()
    {
        Products = new List<Product>();
    }

    public virtual void AddProduct(Product product)
    {
        Products.Add(product);
    }
}
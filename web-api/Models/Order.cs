namespace web_api.Models;

public class Order
{
    public Order()
    {
        Products = new List<Product>();
    }

    public virtual int Id { get; set; }
    public virtual User User { get; set; }
    public virtual IList<Product> Products { get; set; } = new List<Product>();
}
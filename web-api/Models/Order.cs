namespace web_api.Models;

public class Order
{
    public virtual int Id { get; set; }
    public virtual User User { get; set; } = new();
    public virtual IList<Product> Products { get; set; } = new List<Product>();
}
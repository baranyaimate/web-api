namespace web_api.Models;

public class Product
{
    public virtual int Id { get; set; }
    public virtual string Name { get; set; } = string.Empty;
    public virtual int Price { get; set; }
    public virtual IList<Order> Orders { get; set; } = new List<Order>();
}
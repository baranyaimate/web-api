namespace web_api.Models;

public class OrderHasProduct
{
    public virtual int Id { get; set; }
    public virtual Order Order { get; set; } = new();
    public virtual Product Product { get; set; } = new ();
}
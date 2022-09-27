namespace web_api.Models;

public class OrderHasProduct
{
    public virtual int Id { get; set; }
    public virtual Order Order { get; set; }
    public virtual Product Product { get; set; }
}
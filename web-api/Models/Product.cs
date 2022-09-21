namespace web_api.Models;

public class Product
{
    public virtual int Id { get; protected set; }
    public virtual string Name { get; set; }
    public virtual int Price { get; set; }
    public virtual DateTime CreatedAt { get; set; }
    public virtual DateTime UpdatedAt { get; set; }
}
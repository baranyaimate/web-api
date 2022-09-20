namespace web_api.Models.DTO;

public class OrderDto
{
    public virtual int UserId { get; set; }
    // TODO: Fix products
    public virtual IList<Product> Products { get; protected set; }
}
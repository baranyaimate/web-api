namespace web_api.Models.DTO;

public class OrderDto
{
    public virtual int UserId { get; set; }
    public virtual int[] ProductIds { get; set; }
}
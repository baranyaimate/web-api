using Newtonsoft.Json;

namespace web_api.Models.DTO;

public class OrderSummaryDto
{
    [JsonProperty("totalPrice")]
    public virtual int TotalPrice { get; set; }
    
    [JsonProperty("totalOrders")]
    public virtual int TotalOrders { get; set; }
    
    [JsonProperty("averageProductPrice")]
    public virtual double AverageProductPrice { get; set; }
    
    [JsonProperty("orders")]
    public virtual IList<Order> Orders { get; set; } = new List<Order>();
}
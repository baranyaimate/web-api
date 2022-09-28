using Newtonsoft.Json;

namespace web_api.Models.DTO;

public class ProductSummaryDto
{
    [JsonProperty("totalSold")]
    public virtual int TotalSold { get; set; }

    [JsonProperty("product")]
    public virtual Product Product { get; set; } = new();
}
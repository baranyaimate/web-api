using Newtonsoft.Json;

namespace web_api.Models.DTO;

public class ProductDto
{
    [JsonProperty("name")]
    public virtual string Name { get; set; }
    
    [JsonProperty("price")]
    public virtual int Price { get; set; }
}
using Newtonsoft.Json;

namespace web_api.Models.DTO;

public class OrderDto
{
    [JsonProperty("userId")] public virtual int UserId { get; set; }

    [JsonProperty("productIds")] public virtual int[] ProductIds { get; set; } = Array.Empty<int>();
}
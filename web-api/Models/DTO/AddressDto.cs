using Newtonsoft.Json;

namespace web_api.Models.DTO;

public class AddressDto
{
    [JsonProperty("country")]
    public virtual string Country { get; set; } = string.Empty;
    
    [JsonProperty("city")]
    public virtual string City { get; set; } = string.Empty;
    
    [JsonProperty("postcode")]
    public virtual string Postcode { get; set; } = string.Empty;
    
    [JsonProperty("state")]
    public virtual string State { get; set; } = string.Empty;
    
    [JsonProperty("streetName")]
    public virtual string StreetName { get; set; } = string.Empty;
    
    [JsonProperty("streetNumber")]
    public virtual string StreetNumber { get; set; } = string.Empty;
    
    [JsonProperty("userId")]
    public virtual int UserId { get; set; }
}
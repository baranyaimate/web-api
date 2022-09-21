using Newtonsoft.Json;

namespace web_api.Models.DTO;

public class AddressDto
{
    [JsonProperty("country")]
    public virtual string Country { get; set; }
    
    [JsonProperty("city")]
    public virtual string City { get; set; }
    
    [JsonProperty("postcode")]
    public virtual string Postcode { get; set; }
    
    [JsonProperty("state")]
    public virtual string State { get; set; }
    
    [JsonProperty("streetName")]
    public virtual string StreetName { get; set; }
    
    [JsonProperty("streetNumber")]
    public virtual string StreetNumber { get; set; }
    
    [JsonProperty("userId")]
    public virtual int UserId { get; set; }
}
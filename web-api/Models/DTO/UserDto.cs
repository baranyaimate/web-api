using Newtonsoft.Json;

namespace web_api.Models.DTO;

public class UserDto
{
    [JsonProperty("firstName")]
    public virtual string FirstName { get; set; } = string.Empty;
    
    [JsonProperty("lastName")]
    public virtual string LastName { get; set; } = string.Empty;
    
    [JsonProperty("email")]
    public virtual string Email { get; set; } = string.Empty;
}
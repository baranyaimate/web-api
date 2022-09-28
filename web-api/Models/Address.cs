namespace web_api.Models;

public class Address
{
    public virtual int Id { get; set; }
    public virtual string Country { get; set; } = string.Empty;
    public virtual string City { get; set; } = string.Empty;
    public virtual string Postcode { get; set; } = string.Empty;
    public virtual string State { get; set; } = string.Empty;
    public virtual string StreetName { get; set; } = string.Empty;
    public virtual string StreetNumber { get; set; } = string.Empty;
    public virtual User User { get; set; } = new();
}
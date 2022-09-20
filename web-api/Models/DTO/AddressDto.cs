namespace web_api.Models.DTO;

public class AddressDto
{
    public virtual string Country { get; set; }
    public virtual string City { get; set; }
    public virtual string Postcode { get; set; }
    public virtual string State { get; set; }
    public virtual string StreetName { get; set; }
    public virtual string StreetNumber { get; set; }
    public virtual int UserId { get; set; }
}
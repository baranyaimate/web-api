namespace web_api.Models;

public class User
{
    public virtual int Id { get; set; }
    public virtual string FirstName { get; set; } = string.Empty;
    public virtual string LastName { get; set; } = string.Empty;
    public virtual string Email { get; set; } = string.Empty;
    public virtual IList<Address> Addresses { get; set; } = new List<Address>();
}
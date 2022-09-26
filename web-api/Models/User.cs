namespace web_api.Models;

public class User
{
    public virtual int Id { get; set; }
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public virtual string Email { get; set; }
    public virtual IList<Address> Addresses { get; set; }
}
namespace web_api.Models
{
    public class Address
    {
        public virtual int Id { get; protected set; }
        public virtual string Country { get; set; }
        public virtual string City { get; set; }
        public virtual string Postcode { get; set; }
        public virtual string State { get; set; }
        public virtual string StreetName { get; set; }
        public virtual string StreetNumber { get; set; }
        public virtual User User { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}
namespace web_api.Models
{
    public class Order
    {
        public virtual int Id { get; protected set; } 
        public virtual User User { get; set; }
        public virtual IList<Product> Products { get; protected set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }

        public Order()
        {
            Products = new List<Product>();
        }

        public virtual void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public virtual void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }
    }
}
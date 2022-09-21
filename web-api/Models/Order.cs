﻿namespace web_api.Models
{
    public class Order
    {
        public virtual int Id { get; protected set; } 
        public virtual User User { get; set; }
        public virtual IList<Product> Products { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }

        public Order()
        {
            Products = new List<Product>();
        }
    }
}
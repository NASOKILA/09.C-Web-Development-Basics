namespace HTTPServer.ByTheCakeApplication.Models
{
    using System;
    using System.Collections.Generic;
    
    public class Order
    {
        public Order()
        {
            this.Products = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime DateOfCreation { get; set; }

        public ICollection<ProductOrder> Products { get; set; }
    }
}

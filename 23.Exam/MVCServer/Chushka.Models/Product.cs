using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chushka.Models
{
    public class Product
    {

        public Product()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        public string Description { get; set; }

        public Chushka.Models.Type Type { get; set; }

        public int TypeId { get; set; }
        
        public ICollection<Order> Orders { get; set; }
        
    }
}

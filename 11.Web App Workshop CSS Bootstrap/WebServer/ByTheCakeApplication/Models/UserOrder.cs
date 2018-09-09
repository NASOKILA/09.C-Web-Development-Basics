namespace HTTPServer.ByTheCakeApplication.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserOrder
    {
        public int UserId { get; set; }

        public int OrderId { get; set; }

        public User User { get; set; }

        public Order Order { get; set; }
    }
}

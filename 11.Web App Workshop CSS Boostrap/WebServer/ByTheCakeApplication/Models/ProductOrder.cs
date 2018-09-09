namespace HTTPServer.ByTheCakeApplication.Models
{
    
    //Tova e klas slujesht kato vruzka mejdu products i orders
    public class ProductOrder
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public Order Order { get; set; }

    }
}

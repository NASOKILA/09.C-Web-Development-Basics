namespace HTTPServer.Application.Views.Cake
{
    
    public class Cake
    {
        public Cake(string name, string price)
        {
            this.Name = name;

            this.Price = price;
        }

        public string Name { get; set; }

        public string Price { get; set; }
    }
}

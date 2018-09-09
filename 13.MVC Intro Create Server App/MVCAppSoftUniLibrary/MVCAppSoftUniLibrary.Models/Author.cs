namespace MVCAppSoftUniLibrary.Models
{
    using System.Collections.Generic;
    
    public class Author
    {
        public Author()
        {
            this.Books = new List<Book>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}

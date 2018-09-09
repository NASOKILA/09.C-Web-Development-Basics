namespace MVCAppSoftUniLibrary.Models
{
    
    public class Book
    {

        public Book()
        {}

        public int Id { get; set; }

        public string Title { get; set; }

        public string CoverImageUrl { get; set; }

        public Author Author { get; set; }

        public int AuthorId { get; set; }
        
    }
}

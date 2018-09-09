namespace MVCAppSoftUniLibrary.Controllers
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Controllers.Interfaces;
    using MVCAppSoftUniLibrary.Views.Books;

    public class BooksController : IBooksController
    {
        private readonly SoftUniLibraryContext context;

        public BooksController(SoftUniLibraryContext context)
        {
            this.context = context;
        }
        
        public string GetAllBooks() {

            var books = context.Books.Include(b => b.Author).ToList();

            if (books.Count < 1)
                return "No boos in the database";

            //I tuk polzvame View
            return new AllView().Display(books);
        }
        
        public string GetBookDetails()
        {
            Console.WriteLine("Select book id:");

            int bookId = int.Parse(Console.ReadLine());
            
            List<Book> books = context.Books.Include(b => b.Author).ToList();

            Book currentBook = books.SingleOrDefault(b => b.Id == bookId);

            if (currentBook == null)
                return "No book with that id.";

            //TUK POLZVAME VIEW
            return new DetailsView().Display(currentBook);
        }
    }
}

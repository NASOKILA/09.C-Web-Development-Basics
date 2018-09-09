namespace MVCAppSoftUniLibrary.Controllers
{
    using Interfaces;
    using Data;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System;
    using Views.Authors;

    public class AuthorsController : IAuthorsController
    {
        private readonly SoftUniLibraryContext context;

        public AuthorsController(SoftUniLibraryContext context)
        {
            this.context = context;
        }

        public string GetAuthorDetails()
        {
            Console.WriteLine("Select author Id:");

            int authorId = int.Parse(Console.ReadLine());
            
            Author author = context
                .Authors
                .Include(a => a.Books)
                .SingleOrDefault(a => a.Id == authorId);

            if (author == null)
                return "No author with that id.";

            //TUK POLZVAME VIEW
            return new DetailsView().Display(author);
        }
    }
}

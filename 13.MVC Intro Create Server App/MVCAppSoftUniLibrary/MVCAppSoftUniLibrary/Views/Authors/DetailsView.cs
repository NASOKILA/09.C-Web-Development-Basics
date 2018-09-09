namespace MVCAppSoftUniLibrary.Views.Authors
{
    using Interfaces;
    using Models;
    using System;
    using System.Linq;

    public class DetailsView : IView
    {
        public string Display(object model)
        {
            var author = model as Author;

            string result = string.Empty;

            result += ($"Id: {author.Id} | Name: {author.Name} | Books:" + Environment.NewLine);
            
            foreach (Book authorBook in author.Books.ToList())
                result += $"    Title: {authorBook.Title}" + Environment.NewLine;

            return result;
        }
    }
}

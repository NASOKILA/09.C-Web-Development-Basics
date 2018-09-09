namespace MVCAppSoftUniLibrary.Views.Books
{
    using Interfaces;
    using Models;
    using System;

    public class DetailsView : IView
    {
        public string Display(object model)
        {

            string result = string.Empty;

            Book currentBook = model as Book;
            
            result += $"Id: {currentBook.Id} | Title: {currentBook.Title} | ImageUrl: {currentBook.CoverImageUrl} | Author: {currentBook.Author.Name}" +
                    Environment.NewLine;

            return result;
        }
    }
}

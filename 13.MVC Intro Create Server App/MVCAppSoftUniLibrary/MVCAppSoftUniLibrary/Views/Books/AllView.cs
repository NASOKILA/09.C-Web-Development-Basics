namespace MVCAppSoftUniLibrary.Views.Books
{
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;

    public class AllView : IView
    {
        public string Display(object model)
        {
            var books = model as List<Book>;

            string result = string.Empty;

            foreach (Book book in books)
                result += ($"Id: {book.Id} | Title: {book.Title}" + Environment.NewLine);

            return result;
        }
    }
}

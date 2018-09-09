namespace MVCAppSoftUniLibrary
{
    using MVCAppSoftUniLibrary.Controllers;
    using MVCAppSoftUniLibrary.Controllers.Interfaces;
    using MVCAppSoftUniLibrary.Data;
    using System;

    public class Router
    {
        //mojehme da go napravim na statichen klas sus statichna funkciq i da nqma nujda ot instanciq

        public void Run(SoftUniLibraryContext context) {

            IBooksController booksController = new BooksController(context);
            IAuthorsController authorsController = new AuthorsController(context);


            //Logikata na prilojenieto
            while (true)
            {

                Console.WriteLine("Please select an action:");
                Console.WriteLine("     \"AllBooks\"  -  Show all books.");
                Console.WriteLine("     \"BookDetails\"  -  Show book details.");
                Console.WriteLine("     \"AuthorDetails\"  -  Show author details.");
                Console.WriteLine("     \"Exit\"  -  Exit the app.");

                //Mojem direktno da podadem dadeni parametri i posle da splitvame !
                string action = Console.ReadLine();

                switch (action)
                {
                    case "AllBooks":
                        Console.WriteLine(booksController.GetAllBooks());
                        break;

                    case "BookDetails":
                        Console.WriteLine(booksController.GetBookDetails());
                        break;

                    case "AuthorDetails":
                        Console.WriteLine(authorsController.GetAuthorDetails());
                        break;

                    case "Exit":
                        ExitTheApp();
                        break;

                    default:
                        Console.WriteLine("Invalid Action!");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("--------------------------------------------------------------------------");
                Console.WriteLine();
            }
        }

        private static void ExitTheApp()
        {
            Console.WriteLine("Bye bye !");
            Environment.Exit(0);
        }

    }
}

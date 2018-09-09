namespace MVCAppSoftUniLibrary
{
    using Data;
    using Microsoft.EntityFrameworkCore;
 
    public class StartUp
    {
        static void Main(string[] args)
        {

            var context = new SoftUniLibraryContext();
            
            //Pravim si bazata
            InitializeDatabase(context);

            InitializeRouter(context);
           
            
        }

        private static void InitializeRouter(SoftUniLibraryContext context)
        {
            Router router = new Router();
            router.Run(context);
        }

        private static void InitializeDatabase(SoftUniLibraryContext context) {

            context.Database.Migrate();
            
        }
        
    }
}

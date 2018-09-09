namespace AsyncAndAwait
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {
            DoSomething();
        }

        static async void DoSomething()
        {

            long result = 0;

            Console.WriteLine("Started");

             await Task.Run(() =>
            {

                Console.WriteLine("Loading...");

                for (int i = 0; i < 1000; i++)
                {
                    result = i;
                }

            result = result * 2;
            });
            
            Console.WriteLine("Finished");
            Console.WriteLine(result);   
        }
    }
}

namespace SumPrimesInRange
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Program
    {
        public static List<int> GenerateNumbers()
        {
            List<int> result = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                Random rnd = new Random();
                int num = rnd.Next(100000, 500000);
                result.Add(num);
            }

            return result;
        }

        static void Main(string[] args)
        {
            long? result = null;

            Task.Run(() =>
            {
                result = GenerateNumbers().Sum();
            })
            .GetAwaiter()
            .GetResult();

            while (true)
            {
                string line = Console.ReadLine();

                if (line == "end" || line == "") 
                    break;

                if (line == "show")
                {
                    if (result.HasValue)
                    {
                        Console.WriteLine(result);
                    }
                    else
                    {
                        Console.WriteLine("Loading...");
                    }
                }
            }
        }
    }
}

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

            //generitrame 10 chisla ot 0 do 1000 i do slagame v spisuka
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

            //Tova e void Task koito ne vrushta nishto no za da 
            //go izpulnim pak ni trqbva .GetAwaiter().GetResult();
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

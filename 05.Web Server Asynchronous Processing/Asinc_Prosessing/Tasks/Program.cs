namespace Tasks
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {
            Task<string> stringTask = Task<string>.Run(() => {

                return ("I AM TASK ONE");
            });

            Console.WriteLine(stringTask.GetAwaiter().GetResult());

            Console.WriteLine(stringTask.Result);

            Task<long> longTask = new Task<long>(() =>
            {
                return 12314423412314;
            });

            longTask.Start();
            Console.WriteLine(longTask.GetAwaiter().GetResult());        
        }
    }
}

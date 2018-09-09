namespace EvenNumbersThread
{
    using System;
    using System.Threading;

    public class Program
    {
        static void Main(string[] args)
        {

            string[] nums = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int start = int.Parse(nums[0]) + 1;
            int end = int.Parse(nums[1]);

            Thread evens = new Thread(() =>
            {
                PrintEvenNumbers(start, end);
            });

            evens.Start(); //puskame threada
            evens.Join(); //izchakvame dokato svurshi i posle produljava koda na dolo

            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int start, int end) {

            for (int i = start; i <= end; i++)
            {
                Console.WriteLine(i);
            }
        }

    }
}

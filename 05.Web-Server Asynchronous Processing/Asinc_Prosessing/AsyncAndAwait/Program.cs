namespace AsyncAndAwait
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {

            /*
             Async & Await:
                Te rabotat zaedno.
                Mojem da slojim async na daden metod koeto kazva na kompilatora che toi moje 
                da se izpulni asinhromno, a tova stawa kato mu slojim await.
                Taka nie mojem da izchakvame nqkakvi taskove da se izpulnqt edin sled drug.

                POVECHE SE POLZVA PRI TASKOVETE.
                SLAGA SE ASYNC NA METOD KOITO SUDURJA TASKOVE A NA TQH IMA SLAGAME AWAIT;

                Te sa kakto v JAVASCRIPT.

            Imame asinhronen kod koito se durji po sinhromno.

             */


            DoSomething();
        }


        //Metoda na koito slagame async trqbva da e void ili Task
        static async void DoSomething()
        {

            //Sega koda izchakva purvo da se izpulni taska i polse produljava nadolo
            //inache ako ne slojim asys i await koda se izpulnqva asinhromno

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
            

            //VAJNO : V takava situaciq mojem prosto da polzvame .GetAwaiter().GetResult() i 
            //koda shte se izpulni SINHROMNO ne ni e nujno async i await.

        }


    }
}

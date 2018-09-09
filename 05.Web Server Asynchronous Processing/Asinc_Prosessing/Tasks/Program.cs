namespace Tasks
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {
            /*
             Taskove:
                 Taska v C# e obekt koito ni uslesnqva asinhromnoto programirane.
                 Te runvat paralelno i se polzvat po lesno ot Threadovete. 

                 Vseki edin task ni dava vuzmojnost da suzdadem zadachka, da i vidim 
                 procesa na rabota i da ni vurne nqkkuv rezultat.

                 Mojem da navurjem nqkolko taska da vurvat paralelno.
                 V tqh mojem da hvashtame i EXEPTIONI.
            */


            //GENERIC TASKOVETE VRUSHTAT REZULTAT V BUDESHTE
            //kak se suzdava task ?
            Task<string> stringTask = Task<string>.Run(() => {

                //MOJEM DA MU DAVAME KAKVITO SI ISKAME ZADACHKI DA IZVURSHVA
                return ("I AM TASK ONE");
            });

            //taka vzimme resultata
            Console.WriteLine(stringTask.GetAwaiter().GetResult());

            //Ili taka ako polzvame .Run()
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

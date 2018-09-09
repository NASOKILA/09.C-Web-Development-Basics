using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Asinc_Prosessing
{
    class Program
    {

        static void LongOperation()
        {
            for (int i = 0; i < 10; i++)
            {
                //S thread Sleep pauzirame programata 
                Thread.Sleep(1000);
            }
        }

        static void Main(string[] args)
        {
            
            /*
             Shte razgledame :
                01.Asinhromno programirane,
                02.Trendovete kato koncepciq, polzvat se pri asinc koda,
                03.Sinhromno programirane
                04.Taskove - suzdavat asinhromni operacii
             */



            /*
                01.Tredove:
                    Asinhromnoto programirane raboti na mnogo nishki svurzani s procesora
                    dokato sihromnoto programirane e samo na edna nishka.
                    Kolkoto qdra imam na procesora tolkova operacii navednuj moga da izpulnqvam.
                    Primerno ako imam 8 qdra moga da izpulnqvam do 8 zadachi navednuj, 
                    nie mojem da pusnem i 100 operacii na vednuj no shte se izpulnqvat po 8 nared.
                    
                    Trendovete sa kato parchenca ot instrukcii koito trqbva da budat 
                    izpulneni posledovatelno.
             
             */

            Console.WriteLine("First");
            
            //Taka se pravi thread, vsichko v nego se izpulnqva paralelno t.e. koda si produljava nadolo.
            Thread thread = new Thread(() =>
            {
                //kogato stignem do treada koda shte si produlji nadolo i ednovremenno shte si vurvi for cikula.
                for (int i = 0; i < 10; i++){
                    Console.WriteLine(i);
                }
            });


            //taka puskame threada, toi NE startira vednaga a kogato windowsa mu pozvoli.
            thread.Start();



            for (int i = 1000; i < 1010; i++)
            {
                Console.WriteLine(i);
            }

            
            //Kato polzvame .Join(); KODUT SPIRA DO TAM, IZPULNQVA SE THREADA I POSLE PRODULJAVA NA DOLO
            thread.Join();

            Console.WriteLine("Second");



            //Threada se izpulnqva paralelno s koda ZATOVA NE SE ZNAE KOE NA KAKUV RED SHTE SE IZPULNI.
            //V TAKUV SLUCHAI NESHTATA NE ZAVISQT EDNO OT DRUGO.
            //VSEKI THREAD SI IMA SOBTVEN STACK V KOITO SE IZPULNQVAT FUNKII

            //LongOperation();   //Nqma da mojem da pishem zashtoto programata shte bude blokirana za 10 sekundi

            //Obache vuv thread shte mojem zashtoto shte raboti paralelno
            Thread LongOperationThread = new Thread(() =>
            {
                LongOperation();
            });


            Console.WriteLine();
            Console.WriteLine();





            /*
             Thread Race Condition:
                Tova se sluchva kogato imame dve nishki koito rabotat s edni danni i ne e qsno kakvo tochno 
                se sluchva i vuv kakuv red.
                V takuv sluchai se hvurlq exception osobeno pri triene na elementi.
                
            */
            
            //Prier:    suzdavame golqm spisuk s 1 milion elementi
            List<int> list = Enumerable.Range(1, 10000).ToList();


            //Sega iskame da iztriem spituka s nqkolko threada zashtoto stava po burzo.

            //Puskame 4 nishki
            for (int i = 0; i < 4; i++)
            {
                new Thread(() =>
                {
                    //kazvame mu dokato lista ima chisla da iztrie poslednoto.

                    /*
                    
                    TOZI PROBLEM S hVUrlqNEto NA ExCEPTION SE RESHAVA KATO POLZVAME 'lock' KeyWorda KOITO DAWA DOSTUP
                    DO ELEMENT SAMO NA EDIN Thread I DOKATO NE GO OTPUSNE DRUGITE THREADOVE SEDQT I CHAKAT.
                
                    */

                    //Trqbva da locknem elementa predi da pochnem rabota s nego.
                    lock (list)
                    {
                        while (list.Count > 0)
                        {
                            //Tuk hvurlq exception AKO NE POLZVAME 'Lock' zashtoto nqkolko nishki se opitvat da iztriqt element koito veche e iztrit   
                            list.Remove(list.Count - 1);
                        }
                    }
                    

                }).Start();

            }


            //DEADLOCK: Tova se poluchava kogato imame 2 locknati threada koito se chakat edna sdruga i nikoga nqma da se locknat.

            /*
                Exceptions:
                    Te ne mogat da se handelvat izvun daden thread.
                    Vseki thread si ima sobstven stack.
                    Exceptionite za da gi hvanem trqbva da sme v sushtiq thread.
             */




            /*
                Sinchronous & Asynchronous Programming:

                    Sinchronous programming - tove ozn da se izpulnqva koda stupka po stupka,
                        edno sled drugo i se polzva samo edin thrad.
                        
                        Loshoto e che mojem da imame 8 qdra koito da ne polzvame i ednovremenno 1 ot tqh da 
                        vurshi cqlata rabota i da se muchi.
                        PRI PO MALKO ZADACHI ZA VURSHENE E PO DOBRE DA POLZVAME Sinhromno Programirane ZASHTOTO E PO BURZO. 


                    Asynchronous Programming - Raboti se na mnogo nishki v procesora,
                        sledovatelno AKO IMAME POVECHE ZADACHI e po burzo.
                        V task managera pri Performance mojem da vidim kolko threada vurvut na 
                        nashiq kompiuter v momenta. 
                        Te sa poveche ot 2000 no procesora ne se natovarva zashtoto golqma chast ot tezi nishki
                        ne pravqt nishto.
                        
                        Primer za tova sa programite za svalqne na filmi ot internet kato Uttorent, bitcomet i drugi.
                        Pozvolqvat ni da teglim mnogo filmi na vednuj i raboti bez problem.
                    
                        Loshoto tuk e che ne znaem koe koga se izpulnqva i v kakuv red.
                        I e po tejuk za debugvane.
                        Po nqkoga trqbva da polzvame 'lock' za da zashtitavame danni i da ne ni gurmi.
                        Moje da se poluchi Deadock

                    NE E QSNO KOE E PO DOBRE, ZAVISI OT SITUACIQTA, DALI IMASH MNOGO TRAFIK (ZADACHKI) ILI NE!!!
                        
             */



            /*
             Taskovete gi vidqhme v drugite failove.
                
            Exeptionite na taskovete:
                Purvi startirame taska
                posle sus task.Wait(); v try i catch
                
            MNOGO PO LESNO I UDOBNO E OT NORMALNITE EXEPTIONI
            
            try
            {
                task.Wait();
            }
            catch(AggregateExeption ex)
            {
                //Handle EXEPTION
            }

             */


            /*
             Za Async & Await vij drugiq proekt.

            Imame vgradeni asinhromni metodi kato : 
            GetAsync()  i  PostAsync() koito sa asinhromni GET i POST zaqvki kum server.
            
            Primerno v Straeamovete imame ReadAsync() i WriteAsync()
            
            */



            /*
                VAJNOOOOOOOOOOOOOOOOOOOOOOOOOOO!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    NAVSQKUDE KUDETO VIDIM async METOD MOJEM DAGO AWAITNEM NA MOMENTA.
             */


            /*
                VIJ DOWNLOADING FILE PROEKTA ZA KAK DA SVALQME FAILOVE.
             
             */



            /*
             TCP - Tova e za slushane i kak raboti samata mreja, socketi i drugo.

            VIJ PROEKTA TCP_Networking!!!

            Kak da napravim server koito da slusha za zaqvki
            Znaem kak raboti servera.

            TCP listener & Socket:
                Tova sa SERVERI i dvede pravqt edno i sushto.
                TCP listener slusha na nqkkuv url i port koito mu zadadem.
                Socket pravi sushtoto samoche ni dava poveche kontrol
                    

            VIJ SimpleWebServer Proekta ZADULJITELNO !!!!!!!!
            
             */

            

            /*
             SUMMARY:
                Vidqjme kakvo e asinhromno programirane i che raboti s
                    threadovete s koito puskame mnogo neshta da se izpulnqvat paralelno
                    bez da zavsqt edno ot drugo.
                
                Async & Await - S tqh izchakvame, asinhronen kod se izpulnqva sinhromno NO BEZ DA GuBIM VREME V 
                    CHAKANE NA PROGRAMATA ZASHTOTO NQMA ZAVISIMOSTI EDNO OT DRUGO.
                    Kudeto imame metodi zavurshvashti na async trqbva da im slojim await.
                    Await ne moje da e bez async, async metodi mogat da sa samo void metodi ILI TASKOVE.    

                Taskove - Mnogo shte gi polzvame pri asinhromno programirame, Task.Run(() => {};).GetAwaiter().GetResult();
                    Kato void metodi sa samoche ni davat poveche kontrol No mojem i da gi napravim da vrushtat neshto Task<int>.

                 

             */

        }

        //Tova shte slusha na nqkoi port koito mu zadadem
        static private async Task TCP_Listener() {

            int port = 8000;
            IPAddress ipAddress = IPAddress.Parse("localhost:");
            TcpListener server = new TcpListener(ipAddress, port);
            server.Start();

            //SUZDADOME SERVER
        }

    }
}

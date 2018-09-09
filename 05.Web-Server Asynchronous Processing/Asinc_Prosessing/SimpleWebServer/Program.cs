namespace SimpleWebServer
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        static void Main(string[] args)
        {

            //Trqbva da si napravim web server, stawa mnogo lesno:
            //MOJEM DA GO NAPISHEM I SUS SOCKET, I DVATA NACHINA SA EDNI I SUSHTi.
            //VAJNO : AKO DADENA STRANICA NQMA FAVIKONKA AVTOMATICHNO IZPRASHTA ZAQVKA KUM favicon.ico ZA DA SI POTURSI

            //V povecheto sluchai kogato polzvame sync metodi gi awaitvame

            //Vzimame si adres (v sluchaq e localhost:)
            IPAddress address = IPAddress.Parse("127.0.0.1");

            //Port
            int port = 1300;

            //Pravim si TCP Listener t.e. samiqt server
            TcpListener server = new TcpListener(address, port);

            //puskame servera
            server.Start();
            Console.WriteLine("Server started.");
            Console.WriteLine($"Listening to TCP clients at 127.0.0.1: {port}");


            //Polzvame Connect metoda za vruzka s klientite
            //Obache trqbva da go izchakame zatova polzvame async & await
            Task.Run(async () => await Connect(server)).Wait();

        }


        //tova e async metod koito se vruzva kum klientite, chete i izprashta danni
        private static async Task Connect(TcpListener listener)
        {

            //pravim vsichko v while cikul za da mogat mnogo klienti zaedno da se vruzvat kum nashiq server
            while (true)
            {

                //Priemame vruzka sus nqkkuv klient obache go awaitvame za da poluchim samiq klient
                //Servera shte sedi v izchakvane na nqkoi da se svurje s nego kato otide na podadeniq ot nas adres.
                var client = await listener.AcceptTcpClientAsync();

                

                //Prochitame requesta ot clienta: polzvame GrtStream() koito ni dava potoka ot danni izprateni ot brawsera
                //purvo ni trqbva byte masiv za da skladirame tam poluchenite baitove ot brawsera
                byte[] request = new byte[1024];
                
                //populvame vsichko v request  masiva, ot 0 do request.length
                //obache ko awaitvame za da go izchakame da svurshi
                await client.GetStream().ReadAsync(request, 0 , request.Length);

                //printirame si na konzolata celiq request
                Console.WriteLine(Encoding.UTF8.GetString(request));



                //Izprashtane na danni kum brawsera
                
                //Purvo prevrushtame texta v baitove koito iskame da izpratim na brawsera
                var data = Encoding.UTF8.GetBytes("Hello From Server!");

                //izprashtame dannite sis WriteAsync()
                await client.GetStream().WriteAsync(data, 0, data.Length);

                //nakraq zaduljitelno osvobojdavame klienta i zatvarqme vruzkata
                Console.WriteLine("Closing connection.");
                client.Dispose();
            }

        }

    }
}

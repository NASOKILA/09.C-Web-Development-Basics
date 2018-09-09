
namespace StateManagementCoockiesAndSesssions
{
    using System;

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            /*
                State management oznachave da mojem da skladirame
                informaciq mejdu klienta i servera chrez sesii i bizkvitki.
                
                VAJNOOOOOOOOO!!!!!!!!!!!!!!!!!
                Coockitata se pazqt v brawsera a sesiqta v sami server.
             

                Cockitata pazqt informaciq kato koi e lognatiqt user.
                Toi e malko parche informaciq.
                
                Servera go izprashta na brawera i ako izleznem i posle pak vleznem, 
                ako ima zapazeno koockie to se izprashta ot brawsera obratno kum servera.
                Mojem da go vidim vuv vseki edin brawser pod Network, Coockie.
                
                I ako go iztriem se razlogvae.
                Ako mojem da kopirame bizkvitkata na nqkoi admin shte mojem da se lognem ot negovo ime.
                    
                Moje da zapazva i drugi neshta svurzani s usera, primerno temi, koshnici,
                preferencii i drugi neshta koito pravi.
                Tezi danni se izprashtat obratno kum servera koito gi skladira, zatova nqkoi danni se zapazvat 
                doti i sus sedmici nared.
                Pazi informaciq i ot formi populneni ot usera.   
                
                
                Kookitata pomagat, HTTP Servera e stateless oznachava che ne moje da skalira informciq mejdu zaqvite na klienta i servera.
                Nqma kak da razpoznae dali dve zaqvki idvat ot edin i sush server, zatova idva na pomosh BIZKVITKATA.

                
                VAJNOOOOOOOOOOOOOOOOOOOOOO!!!!!!!!!!!!!!!!!!!
                Kak se setva Coockieto ? 
                Setva se v RESPONSA, slaga se kluch 'Set-Cookie:' i stoinost samata informaciq koqto iskama da skladirame.
                SLED KATO SME GO SETNALI BRAWSERA GO IZPRASHTA OBRATNO KUM SERVATA NA VSQKA ZAQVKA.
                
            
                FORMAT NA COOCKITO:
                    Coockitata imat Kluch Stoinot Atributi, neshto kato JSON format, samoche sa razdeleni sus ';'
                    Imat si ime, Durjat informaciq za primerno ezika na stranicata, put na stranicata KOGA DA SE IZPRASHTA OBRATNO NA SERVARA, 
                    koga izticha smoto coockie, DOMAIN - NA KOI WEBSAIT ISKAME DA SE IZPRASHTA TOVA KOOKIE.
            


                Coockie LifeTime:
                    Koga iztichat tova koockie ? 
                    Ima dva nachina:
                        'Epires' - Kazvame mu koga da se iztrie kato data, PO DEFAULT SE TRIQT KOGATO SESIQTA PRIKLUCHI.
                        'Max-Age' - Interval ot sekundi predi da se iztrie tova kookie, t.e. SLAGAME MU MAKSIMALEN JIVOT I POSLE SAMO SE IZTRIVA,
                            ZATOVA KOGATO SI ZABRAVIM KOMPIUTURA NA EDNA STRANICA SUS CHASOVE POSLE KATO REFRESHNEM VECHE NE SME LOGNATI.
                            MOJEM DA GO SETNEM DA E VECHNO KOOKIETO, TOVA SE PRAVI KOGATO PRIMERNO USERA KLIKNE NA 'Remember Me'
             
                Coockie Security:
                    'Security' - Kazvame na brawsera da polzva kookieta samo ot HTTPS protokol koito e kriptiran.
                    'HttpOnly' - Taka mu kazvame che NE moje da se dostupi ot JAVASCRIPT.
             

                Third Party Coockie:
                    Mojem da setnem brawsera da ne ni izprashta kookito obratno na nas a da go izprashta na drug server.
                    Po princip se polzva za reklami koito idvat ot drugi serveri i kato brawsera gi izprati obratno na tqh, te znaqt che nqkoi 
                    e kliknal na tehnata reklama.

            
                KAK SE PRAVI KOOKIE ?
                    Tova e normalen klas sus normalni propertita i stoinosti.
                
                
                




                SESSIONS:
                    Tova e oshte edin nachin za zapazvane na informaciq mejdu requet i response obache tazi informaciq ostavq na servera i toq q pazi.
                    Primerno, ako ne iskame brawsera za znae za tazi informaciq ili ako tq e trurde golqma mojem da q zapazim na servera.
                    
                    Aplikaciqta suzdava sesiq za tekushtiq user i generira ID i sudurja informaciq za dadenata stranica.
                    Pravehme takiva aplikacii sus HANDLEBARS I SAMMY.
                    
                    SESIQTA E PAK SUS SUSHTIQ FORMAT, KLUCH I STOINOST KOITO E JSON OBEKT, ZNAE ZA KOE ID KAKVO SUOTVETSTVA.

                    Sesiqta pak se pravi kato normalen sait.
                    
            

                SUMMARY :
                    Kookitata se pazqt v brawzera te sa s format kluch stoinost, 
                        setvat se v response zaqvka s daden kluch,
                        Sled tova brawsera na vseki request zapochva da go izrashta kum servera.
                    
                    Sesiite sa podobni samoche informaciqta se suhranqva na survera.
                    
             */ 
        }
    }
}

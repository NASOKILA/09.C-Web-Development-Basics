

/*
Kak raboti procesa na edin server ?

V launchera imame Run() metod v koito si inicializirame aplikaciqta,

konfigurirame routinga, 
suzdavame servera da raboti na daden port i go puskame.

HTTP e protokol za vruzka i komunikaciq mejdu klient i server.


Nai vjnoto e da si imame klas Connection Handler koito da ni obrabotva vruzkata sus klinta,
trqbva da imame klas koito da ni parsva request-a,
klas koito da ni go durgi tozi request, i klas koito da ni durji responsa.

Vsichki ostanali klasove sa samo iznasqne na abstrakcii i konretiki.

CQLATA INFORMACIQ ZA REQUEST I RESPONSE SE SUDURJA V RAZPREDELENI KLASOVE ZA DA BUDE PO ESNA OBRABOTKATA!

Imame klas WebServer koito da durji informaciq za port, localhost string adre, da ima TcpListener, 
cqata routing konfiguraciq veche napravena i dr.

V ConnectionHandler imame klient koti go skladirame v propertu ot tip Socket.

Cookitata i Sessionite sa shearnati mejdu Requesta i Responsa.


Tcp kato cqlo e drug protokol.


TcpListener e klas koito slusha za vruzka, no e mnogo slojen.

Servera trqbva da e asinhronen za da moje nqkoolko choveka da go polzvat navednuj,
AKO E SAMO SINHRONEN NQMA DA MOJE KLIENT 2 DA SE SVURJE ZASHTOTO SERVERA OBRABOTVA OSHTE KLIENT 1
I SHTE TRQBVA DA CHAKA.


chaka se za nov klient v asinhronen metod, samiq request se chete asinhromno, 

Dannite ot klienta se vzimat kato masiv ot baitove i gi skladirame v new byte[1024];
Tezi danni se vzimat v async metod.
baitovete se encodvat s Encoding.UTF8.GetString(); i gi poluchavame kato string i veche mojem da 
zapochnem da parsvame.

Za Responsa pravim obratnoto, formirame response koito e string i go prevrushtame v masiv ot baitove koito
izprashtame na brawsera sus    client.SendAsync()   i nakraq socketa trqbva da go obiem zashtoto responsa veche e vurnat.



Routovata konfiguraciq kazva na saniq server kakvo da vurne pri daden rout inache shte vrushta edno i sushto na vseki rout.

Imame AppRouteConfig Fail v koito imame Dictionary<route, handler>  koito sudurja kato kluch daden rout i kato value
	daden hndler koito trqbva da se izoulni.
	I taka zapisvame vsichki routove i tehnite handleri.
	
	Vseki handler si ima method koito da vrushta dadeno view.

	Vsichki tezi routove gi pravim v aolikaciqta i posle kazvam na rservera da gi polzva.

*/

























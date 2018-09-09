/*MVC :
	model view controller
	Tova e :
		01.arhitektura - tova e MVC arhitektura
		02.design pattern - tova ozn mnogo neshta, moje da shablon, moje da imame mnogo klasove v drugi klasove.
	
	Model - durji dannite manipulira se ot kontrollerite, is updeitva viewtata.
	View - tova e html-a ili front end-a i se vijda ot usera.
	Controller - pravi cqlata logika manipulira dannite on modela, 
		priema zapovedi ot samiq user koito go polzva, moje da priema danni ot viewto suhto.	

	Modelite sa glupavi klasove bez logika v tqh, samo chakat da budat izolzvani,
	te sa DAO - Data Access Object.
			
		
	
	Za eventite v C# imame :
		01.event emmitter - TOVA KOETO PRASHTA EVENTA 
		02.event handler - TOVA KOETO HANDELVA EVENTA
		03. loop - TOVA KOETO SVURZVA event emmittera i event handlera, mojem da imame mnogo handleri 
	CQLOTO NESTHTO PRILICHA MALKO NA SMAILI FACE.
	
	Eventite v C#polzvat delegati.
		
		
	Ima i drugi arhitekturi kato MVVM, MV* - tova oznachava vsichki takiva patterni, ima mnogo.
		MVP - Model View PResenter - tuka presentera e kato kontroller i moje da manipulira i View i Model.
		Modela ne moje da updeitva viwto, trqbva da mine prez presentera.
		Presentera ne e kato kontroller, malko po glupav e.
		
		MVVM - Model Viwe ViewModel - Pak imame modeli i viewta, vsichko minava prez ViewModela,
		v koito imame klasove, unit testove, obrabotka na danni i rabota s bazata.
		Vruzkata mejdu modela i viewModela e NEDIREKTNA, t.e. pravi se s EVENTI.
		
		
		Observable Objects:
			Tova sa obekti (klasove) koito pri promqna hvurlqt event che sa promeneni.
			
		
		
		
		
	MVC i Design pattern NE se polzva samo za WEB, tova si e prosto logika za
		rezpredelenie na danni.

	
	
	VAJNOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		Binding Model e obratnoto na ViewModel.




	VAJNOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!!!!!!!!!!!!!!!!!!!!!!!!!!!!
	4 Glavni principi na OOP:
		01.Encapsulation
		02.Inheritance
		03.Polymorphysum
		04.Abtraction
		

		

	VAJNOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		Kogato dobavqme novi proekti kum edin solution dannite, modelite i samata aplikaciq stawat nezavisimi edin ot drug,
		sledovatelno mojem da si pravim mnogo aplikacii koito da polzvat edni i sushti danni i edna i sushta logika. 
		Mojem da pravim i drugi tipove prilojeniq, ne samo konzolni.
		
		Kato dobavqm nov proekt v edin i sushti solution trqbva da mu dobavim referenciq.




	KOGATO PRAVIM INTERFEISI, KONTROLLERI KOITO POLZVAT I SERVISI DAVAME PODREDBA I PO MALKO
	ZAVISOMOST NA KOGA KOETO E MNOGO DOBRE.
	
	Po princip samiqt server trqbva da e otdeen ot prilojenieto i samiq Framework: kontrolleri modeli, servisi, Interfeisi i dr.


	MVC e mnogo udobna arhitektura koqto ni pozvolqva da rabotim nqkolko choveka na razlichni metabez da si prechim i
	e lesno za debugvane i za testvane

*/
		
		
		
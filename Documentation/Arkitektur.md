# Arkitekturbeskrivning

## Översikt

Lösningen är uppdelad enligt en enkel trelagerstruktur:

- Presentation: `MainWindow.xaml`
- Presentationslogik: `MainViewModel`
- Applikationslogik och dataåtkomst: `ForeningService`
- Databasåtkomst: `ForeningDbContext`
- Domänmodell: `Medlem`, `Spel`, `SpelTraff`, `Forening`

## Motivering av arkitektur

Lösningen är uppdelad i presentation, applikationslogik och dataåtkomst för att skapa en tydlig ansvarsfördelning. Detta gör koden enklare att underhålla, testa och vidareutveckla. Genom att hålla databaskoden i `ForeningService` och `ForeningDbContext` undviks koppling mellan användargränssnittet och databasen, vilket följer principerna för MVVM och trelagerarkitektur.

## Presentation

`MainWindow.xaml` ansvarar för visning av listor, inmatningsfält och knappar. Vyn använder databindning mot `MainViewModel` och anropar inte databasen direkt.

## ViewModel

`MainViewModel` håller applikationens tillstånd, till exempel valda objekt, textfält och samlingar. Den exponerar commands som vyn binder till.

## Service

`ForeningService` kapslar in arbetsflöden som att registrera medlem, skapa spelträff, anmäla deltagare och ta bort objekt. Serviceklassen använder `ForeningDbContext`, vilket gör att vyn och ViewModel inte behöver känna till detaljerna om databasen.

## Dataåtkomst

`ForeningDbContext` konfigurerar Entity Framework, relationer och anslutningen till SQL Server LocalDB. Databasen skapas med `EnsureCreatedAsync()`.

## Inkapsling och roller

Domänmodellen har stärkts genom tydligare inkapsling. `Medlem` har privata setters och styr ändringar genom metoder som `Sparra`, `Avsparra` och `UppdateraKontaktuppgifter`.

Alla medlemmar representeras av samma klass `Medlem`, men de kan ha olika roller, till exempel `Spelare`, `Arrangör` och `Administratör`. Det gör modellen enklare att förklara samtidigt som systemet kan skilja på medlemmarnas ansvar.

Varje medlem har ett unikt `Id` som fungerar som medlemsnummer och primärnyckel i databasen. Nya medlemmar får id 1, 2, 3 och så vidare via service-lagret innan de sparas. E-post är inte en främmande nyckel eftersom den inte pekar på en annan tabell, men den är konfigurerad som unik alternativ nyckel. Mobilnummer är också unikt via databasindex. Mobilnummer valideras som en affärsregel: det måste bestå av exakt 10 siffror och börja med 0.

`Spel` och `SpelTraff` har också egna primärnycklar i databasen. Dessa id:n visas i gränssnittet genom objektens `ToString()` så att användaren kan se vilken post som är vald.

Förnamn och efternamn valideras också som affärsregler i domänmodellen. De får bara innehålla bokstäver, mellanslag och bindestreck, vilket hindrar symboler i medlemsnamn.

Spel kan uppdateras genom en metod i domänklassen `Spel`, vilket gör att ändringar av titel, kategori och antal spelare fortfarande går genom klassens validering. Spelträffar skapas via ett kalenderfält för datum och en tidslista, vilket ger ett tydligare och mer användarvänligt arbetsflöde än att skriva hela datum/tid manuellt. Systemet stoppar också spelträffar som ligger i dåtid.

När en spelträff tas bort används `ForeningService` för att ta bort posten från databasen via Entity Framework. Borttagningen är permanent i nuvarande version. Det finns ingen separat audit- eller historiktabell för raderade spelträffar, vilket är ett medvetet avgränsat designval i denna version.

## Designval

Appen använder en liten egen implementation av `RelayCommand` och `AsyncRelayCommand` i stället för CommunityToolkit.Mvvm, eftersom det räcker för projektets omfattning och gör commands lätta att förklara vid redovisning.

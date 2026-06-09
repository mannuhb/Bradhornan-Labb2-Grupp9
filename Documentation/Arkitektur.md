# Arkitekturbeskrivning

## Översikt

Lösningen är uppdelad enligt en enkel trelagerstruktur:

- Presentation: `MainWindow.xaml`
- Presentationslogik: `MainViewModel`
- Applikationslogik och dataåtkomst: `ForeningService`
- Databasåtkomst: `ForeningDbContext`
- Domänmodell: `Medlem`, `StandardMedlem`, `HedersMedlem`, `Spel`, `SpelTraff`, `Forening`

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

## Inkapsling, arv och polymorfism

Domänmodellen har stärkts genom tydligare inkapsling. `Medlem` har privata setters och styr ändringar genom metoder som `Sparra`, `Avsparra` och `UppdateraKontaktuppgifter`.

`StandardMedlem` och `HedersMedlem` är subklasser som ärver från `Medlem`. Det gör att olika medlemstyper kan behandlas gemensamt genom basklassen `Medlem`, men ändå ha möjlighet till olika beteenden genom polymorfism.

Unika fält för medlemsnummer, e-post och mobilnummer skyddas både i service-lagret och med unika index i databasen.

## Designval

Appen använder en liten egen implementation av `RelayCommand` och `AsyncRelayCommand` i stället för CommunityToolkit.Mvvm, eftersom det räcker för projektets omfattning och gör commands lätta att förklara vid redovisning.

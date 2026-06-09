# Brädhörnan - Laboration 2

## Beskrivning

Detta är en WPF-applikation för att hantera en brädspelsförening. Laboration 2 bygger vidare på domänmodellen från Laboration 1 och lägger till MVVM, databindning, commands, Entity Framework och persistent lagring.

Systemet hanterar:

- medlemmar
- brädspel
- spelträffar
- anmälan och avanmälan till spelträffar
- spärrade medlemmar
- filtrering, sortering och gruppering med LINQ

## Tekniker

- C#
- WPF
- MVVM
- Databindning
- Commands
- ObservableCollection
- Entity Framework Core med SQL Server LocalDB
- Async/await vid databasladdning och databasoperationer
- LINQ

## Projektstruktur

- `Models` innehåller domänklasserna `Medlem`, `Spel`, `SpelTraff` och `Forening`.
- `ViewModels` innehåller `MainViewModel`, `ViewModelBase`, `RelayCommand` och `AsyncRelayCommand`.
- `Services` innehåller applikationslogik och databasnära arbetsflöden via `ForeningService`.
- `Data` innehåller `ForeningDbContext` och Entity Framework-konfiguration.
- `MainWindow.xaml` är vyn och använder databindning och commands.
- `MainWindow.xaml.cs` innehåller bara vyteknisk startkod och koppling till `DataContext`.

## Databas

Applikationen använder Entity Framework Core med SQL Server LocalDB:

```text
Server=(localdb)\MSSQLLocalDB;Database=BradhornanDb;Trusted_Connection=True;
```

Databasen skapas automatiskt med `EnsureCreatedAsync()` när applikationen startar. Om databasen är tom läggs demonstrationsdata in från `DemoData`.

## Centrala funktioner

- Visa medlemmar, spel och spelträffar.
- Registrera ny medlem.
- Avregistrera medlem.
- Spärra och avspärra medlem.
- Registrera nytt spel.
- Registrera ny spelträff.
- Anmäla vald medlem till vald spelträff.
- Avanmäla deltagare från spelträff.
- Ta bort vald spelträff.
- Visa aktiva medlemmar.
- Sortera medlemmar alfabetiskt.
- Visa spelträffar med lediga platser.
- Gruppera spel efter kategori.

## Affärsregler

- Medlemsnummer måste vara större än 0.
- Förnamn, efternamn, e-post, mobilnummer, titel, kategori, plats och träffnamn får inte vara tomma.
- Medlemsnummer, e-postadress och mobilnummer måste vara unika.
- Maxantal spelare eller deltagare får inte vara mindre än minantal.
- En spelträff måste följa valt spels min- och maxantal spelare.
- En spärrad medlem kan inte anmälas till spelträff.
- En medlem kan inte anmälas flera gånger till samma spelträff.
- En full spelträff kan inte ta emot fler deltagare.
- En medlem kan bara avanmäla sig från en träff där medlemmen faktiskt är anmäld.

## MVVM

`MainWindow.xaml` binder till `MainViewModel`. Knapptryckningar hanteras via commands, till exempel:

- `RegistreraMedlemCommand`
- `RegistreraSpelCommand`
- `RegistreraSpeltraffCommand`
- `AnmalCommand`
- `AvanmalCommand`
- `TaBortSpeltraffCommand`

`MainWindow.xaml.cs` innehåller inte applikationslogik. Den skapar bara ViewModel, sätter `DataContext` och laddar data när fönstret öppnas.

## Inkapsling och arv

`Medlem` är en central klass i domänmodellen. Varje medlem får ett unikt medlemsnummer som fungerar som medlems-id. Klassens viktiga data har privata setters och ändras via metoder som `Sparra`, `Avsparra` och `UppdateraKontaktuppgifter`. Detta minskar risken för att objekt manipuleras direkt utifrån utan att affärsregler kontrolleras.

Alla medlemmar är vanliga medlemmar, men de kan ha olika roller, till exempel `Spelare`, `Arrangör` eller `Administratör`.

## Asynkronitet

Databasoperationer körs asynkront via `async`/`await`, till exempel laddning av medlemmar, spel och spelträffar samt registrering och anmälan. Detta gör att databasarbete inte behöver ligga direkt i vyn.

## LINQ

LINQ används bland annat för:

- sortering av medlemmar
- filtrering av aktiva medlemmar
- filtrering av spelträffar med lediga platser
- gruppering av spel efter kategori
- sorterad visning av spelträffar

## Hur man kör programmet

1. Öppna `ObjektLab1.sln` i Visual Studio.
2. Kontrollera att NuGet-paket återställs.
3. Starta projektet med Debug.
4. Testa exempelvis arbetsflödet: skapa medlem, skapa spelträff, anmäl medlem, stäng appen och starta igen för att se att data finns kvar.

## Viktigt inför inlämning

Ta inte med genererade mappar i zip-filen:

- `.vs`
- `bin`
- `obj`

## Dokument som hör till inlämningen

- `Documentation/Klassdiagram.svg`
- `Documentation/Sekvensdiagram.svg`
- `Documentation/UseCases.md`
- `Documentation/Reflektion.md`
- `Documentation/Arkitektur.md`

## Författare

- Manar Abdullahi Nuur och Elmi Abdi
- Gruppnummer 9

# Use cases

System: Brädhörnan

Huvudaktör: Administratör/Föreningsansvarig

## Use case 1 - Registrera ny medlem

Aktör: Administratör/Föreningsansvarig

1. Användaren skriver medlemmens förnamn, efternamn, e-postadress och mobilnummer.
2. Användaren klickar på "Registrera ny medlem".
3. Systemet kontrollerar att förnamn, efternamn, e-postadress och mobilnummer är giltiga.
4. Systemet kontrollerar att mobilnummer består av exakt 10 siffror och börjar med 0.
5. Systemet kontrollerar att e-postadress och mobilnummer inte redan används.
6. Systemet skapar ett nytt unikt medlems-id som fungerar som primärnyckel i databasen.
7. Systemet sparar medlemmen i databasen.
8. Medlemslistan uppdateras i gränssnittet.

## Use case 2 - Uppdatera medlem

Aktör: Administratör/Föreningsansvarig

1. Användaren väljer en medlem i medlemslistan.
2. Systemet fyller formuläret med medlemmens nuvarande uppgifter.
3. Användaren ändrar förnamn, efternamn, e-postadress, mobilnummer eller roll.
4. Användaren klickar på "Uppdatera vald medlem".
5. Systemet kontrollerar att förnamn och efternamn bara innehåller bokstäver, mellanslag och bindestreck.
6. Systemet kontrollerar att mobilnummer består av exakt 10 siffror och börjar med 0.
7. Systemet kontrollerar att e-postadress och mobilnummer inte används av någon annan medlem.
8. Systemet sparar ändringarna i databasen.
9. Medlemslistan uppdateras i gränssnittet.

## Use case 3 - Skapa spelträff

Aktör: Administratör/Föreningsansvarig

1. Användaren fyller i namn, plats, datum via kalender, tid och deltagargränser.
2. Användaren väljer ett spel.
3. Användaren klickar på "Registrera ny spelträff".
4. Systemet kontrollerar att inmatningen är giltig.
5. Systemet kontrollerar att datum och tid ligger i framtiden.
6. Systemet kontrollerar att träffens min- och maxdeltagare passar spelets regler.
7. Systemet sparar spelträffen i databasen.
8. Listan med spelträffar uppdateras.

## Use case 4 - Uppdatera spel

Aktör: Administratör/Föreningsansvarig

1. Användaren väljer ett spel i spellistan.
2. Systemet fyller formuläret med spelets nuvarande uppgifter.
3. Användaren ändrar titel, kategori eller antal spelare.
4. Användaren klickar på "Uppdatera valt spel".
5. Systemet kontrollerar att titel och kategori är ifyllda.
6. Systemet kontrollerar att min- och maxantal spelare är giltiga.
7. Systemet sparar ändringen i databasen.
8. Spellistan uppdateras.

## Use case 5 - Anmäla medlem till spelträff

Aktör: Administratör/Föreningsansvarig

1. Användaren väljer en medlem.
2. Användaren väljer en spelträff.
3. Systemet visar validering för vald spelträff, till exempel spel, kategori, deltagarkrav och lediga platser.
4. Användaren klickar på "Anmäl medlem till spelträff".
5. Systemet kontrollerar att medlemmen inte är spärrad.
6. Systemet kontrollerar att medlemmen inte redan är anmäld.
7. Systemet kontrollerar att spelträffen inte är full.
8. Systemet sparar anmälan i databasen.
9. Deltagarlistan, lediga platser och valideringsrutan uppdateras.

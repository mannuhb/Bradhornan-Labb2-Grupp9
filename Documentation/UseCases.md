# Use cases

System: Brädhörnan

Huvudaktör: Administratör/Föreningsansvarig

## Use case 1 - Registrera ny medlem

Aktör: Administratör/Föreningsansvarig

1. Användaren skriver medlemmens namn, e-postadress och mobilnummer.
2. Användaren klickar på "Registrera ny medlem".
3. Systemet kontrollerar att namn, e-postadress och mobilnummer är giltiga.
4. Systemet kontrollerar att e-postadress och mobilnummer inte redan används.
5. Systemet skapar ett nytt unikt medlemsnummer.
6. Systemet sparar medlemmen i databasen.
7. Medlemslistan uppdateras i gränssnittet.

## Use case 2 - Skapa spelträff

Aktör: Administratör/Föreningsansvarig

1. Användaren fyller i namn, plats, datum/tid och deltagargränser.
2. Användaren väljer ett spel.
3. Användaren klickar på "Registrera ny spelträff".
4. Systemet kontrollerar att inmatningen är giltig.
5. Systemet kontrollerar att träffens min- och maxdeltagare passar spelets regler.
6. Systemet sparar spelträffen i databasen.
7. Listan med spelträffar uppdateras.

## Use case 3 - Anmäla medlem till spelträff

Aktör: Administratör/Föreningsansvarig

1. Användaren väljer en medlem.
2. Användaren väljer en spelträff.
3. Användaren klickar på "Anmäl vald medlem".
4. Systemet kontrollerar att medlemmen inte är spärrad.
5. Systemet kontrollerar att medlemmen inte redan är anmäld.
6. Systemet kontrollerar att spelträffen inte är full.
7. Systemet sparar anmälan i databasen.
8. Deltagarlistan och lediga platser uppdateras.

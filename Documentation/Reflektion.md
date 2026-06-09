# Reflektion

Lösningen från Laboration 1 har vidareutvecklats genom att samma centrala domänmodell används, men presentationslogiken har flyttats från code-behind till en ViewModel. Detta gör att vyn blir enklare och att arbetsflödena blir lättare att följa.

Applikationen är indelad i presentation, ViewModel, services, dataåtkomst och modeller. `ForeningService` fungerar som mellanlager mellan ViewModel och Entity Framework. På så sätt behöver gränssnittet inte känna till hur databasen fungerar.

Persistent lagring har lagts till med Entity Framework Core och SQL Server LocalDB. Databasen skapas automatiskt vid start och demonstrationsdata läggs in om databasen är tom.

Ett viktigt designval är att behålla affärsregler i domänklasserna, särskilt i `SpelTraff.Anmal` och `SpelTraff.Avanmal`. Det gör att reglerna inte bara finns i gränssnittet, utan följer objektmodellen.

Vi har även förbättrat inkapslingen i domänmodellen genom att minska direkt åtkomst till medlemsdata och i stället använda tydliga metoder för ändringar. Genom att lägga till `StandardMedlem` och `HedersMedlem` har vi ett enkelt exempel på arv och polymorfism utan att göra modellen onödigt komplicerad.

Asynkrona metoder används vid databasoperationer för att separera databasarbete från vyn och visa responsivitetsaspekten som efterfrågas i Laboration 2.

Arbetet har gett förståelse för hur en domänmodell kan vidareutvecklas till en komplett applikation. Vi har fått praktisk erfarenhet av MVVM, databindning och Entity Framework Core. Vi har också sett värdet av att separera presentation, affärslogik och dataåtkomst för att göra lösningen mer underhållbar.

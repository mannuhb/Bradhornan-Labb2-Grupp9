# Sekvensflöde - Anmäla medlem till spelträff

1. Användaren väljer medlem och spelträff i `MainWindow`.
2. `MainWindow` binder valet till `MainViewModel.ValdMedlem` och `MainViewModel.ValdTraff`.
3. Användaren klickar på knappen "Anmäl vald medlem".
4. Knappen kör `AnmalCommand` i `MainViewModel`.
5. `MainViewModel` anropar `ForeningService.AnmalAsync`.
6. `ForeningService` hämtar vald spelträff och vald medlem med Entity Framework.
7. `SpelTraff.Anmal` kör affärsreglerna.
8. Om reglerna godkänns sparar `ForeningService` ändringen med `SaveChangesAsync`.
9. `MainViewModel` laddar om spelträffar och uppdaterar status.
10. `MainWindow` visar uppdaterade deltagare via databindning.

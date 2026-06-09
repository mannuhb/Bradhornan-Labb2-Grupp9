using System.Collections.ObjectModel;
using System.Text;
using ObjektLab1.Models;
using ObjektLab1.Services;

namespace ObjektLab1.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ForeningService _service = new();

        private Medlem? _valdMedlem;
        private Medlem? _valdDeltagare;
        private Spel? _valtSpelForRedigering;
        private Spel? _valtSpel;
        private SpelTraff? _valdTraff;
        private string _statusText = "Redo.";
        private string _nyMedlemNamn = string.Empty;
        private string _nyMedlemEfternamn = string.Empty;
        private string _nyMedlemEpost = string.Empty;
        private string _nyMedlemMobilnummer = string.Empty;
        private string _nyMedlemRoll = "Spelare";
        private string _nyttSpelTitel = string.Empty;
        private string _nyttSpelKategori = string.Empty;
        private string _nyttSpelMin = string.Empty;
        private string _nyttSpelMax = string.Empty;
        private string _nyTraffNamn = string.Empty;
        private string _nyTraffPlats = string.Empty;
        private DateTime? _nyTraffDatum = DateTime.Today;
        private string _nyTraffTid = "18:00";
        private string _nyTraffMin = string.Empty;
        private string _nyTraffMax = string.Empty;
        private string _grupperingText = string.Empty;

        public ObservableCollection<Medlem> Medlemmar { get; } = new();
        public ObservableCollection<Spel> Spel { get; } = new();
        public ObservableCollection<SpelTraff> Speltraffar { get; } = new();
        public ObservableCollection<Medlem> Deltagare { get; } = new();
        public DateTime MinTraffDatum { get; } = DateTime.Today;
        public ObservableCollection<string> Tider { get; } = new()
        {
            "08:00",
            "08:30",
            "09:00",
            "09:30",
            "10:00",
            "10:30",
            "11:00",
            "11:30",
            "12:00",
            "12:30",
            "13:00",
            "13:30",
            "14:00",
            "14:30",
            "15:00",
            "15:30",
            "16:00",
            "16:30",
            "17:00",
            "17:30",
            "18:00",
            "18:30",
            "19:00",
            "19:30",
            "20:00",
            "20:30",
            "21:00",
            "21:30",
            "22:00"
        };
        public ObservableCollection<string> Roller { get; } = new()
        {
            "Spelare",
            "Arrangör",
            "Administratör"
        };

        public MainViewModel()
        {
            LaddaCommand = new AsyncRelayCommand(LaddaAsync);
            RegistreraMedlemCommand = new AsyncRelayCommand(RegistreraMedlemAsync);
            UppdateraMedlemCommand = new AsyncRelayCommand(UppdateraMedlemAsync, () => ValdMedlem != null);
            AvregistreraMedlemCommand = new AsyncRelayCommand(AvregistreraMedlemAsync, () => ValdMedlem != null);
            SparraMedlemCommand = new AsyncRelayCommand(() => UppdateraMedlemStatusAsync(true), () => ValdMedlem != null);
            AvsparraMedlemCommand = new AsyncRelayCommand(() => UppdateraMedlemStatusAsync(false), () => ValdMedlem != null);
            VisaAktivaMedlemmarCommand = new AsyncRelayCommand(VisaAktivaMedlemmarAsync);
            SorteraMedlemmarCommand = new AsyncRelayCommand(SorteraMedlemmarAsync);
            RegistreraSpelCommand = new AsyncRelayCommand(RegistreraSpelAsync);
            UppdateraSpelCommand = new AsyncRelayCommand(UppdateraSpelAsync, () => ValtSpelForRedigering != null);
            GrupperaSpelCommand = new RelayCommand(GrupperaSpel);
            RegistreraSpeltraffCommand = new AsyncRelayCommand(RegistreraSpeltraffAsync);
            AnmalCommand = new AsyncRelayCommand(AnmalAsync, () => ValdMedlem != null && ValdTraff != null);
            AvanmalCommand = new AsyncRelayCommand(AvanmalAsync, () => ValdDeltagare != null && ValdTraff != null);
            VisaLedigaTraffarCommand = new AsyncRelayCommand(() => LaddaTraffarAsync(true));
            VisaAllaTraffarCommand = new AsyncRelayCommand(() => LaddaTraffarAsync(false));
            TaBortSpeltraffCommand = new AsyncRelayCommand(TaBortSpeltraffAsync, () => ValdTraff != null);
        }

        public AsyncRelayCommand LaddaCommand { get; }
        public AsyncRelayCommand RegistreraMedlemCommand { get; }
        public AsyncRelayCommand UppdateraMedlemCommand { get; }
        public AsyncRelayCommand AvregistreraMedlemCommand { get; }
        public AsyncRelayCommand SparraMedlemCommand { get; }
        public AsyncRelayCommand AvsparraMedlemCommand { get; }
        public AsyncRelayCommand VisaAktivaMedlemmarCommand { get; }
        public AsyncRelayCommand SorteraMedlemmarCommand { get; }
        public AsyncRelayCommand RegistreraSpelCommand { get; }
        public AsyncRelayCommand UppdateraSpelCommand { get; }
        public RelayCommand GrupperaSpelCommand { get; }
        public AsyncRelayCommand RegistreraSpeltraffCommand { get; }
        public AsyncRelayCommand AnmalCommand { get; }
        public AsyncRelayCommand AvanmalCommand { get; }
        public AsyncRelayCommand VisaLedigaTraffarCommand { get; }
        public AsyncRelayCommand VisaAllaTraffarCommand { get; }
        public AsyncRelayCommand TaBortSpeltraffCommand { get; }

        public Medlem? ValdMedlem
        {
            get => _valdMedlem;
            set
            {
                if (SetProperty(ref _valdMedlem, value))
                {
                    FyllMedlemsformularFranValdMedlem();
                    UppdateraCommandStatus();
                }
            }
        }

        public Medlem? ValdDeltagare
        {
            get => _valdDeltagare;
            set
            {
                if (SetProperty(ref _valdDeltagare, value))
                    UppdateraCommandStatus();
            }
        }

        public Spel? ValtSpel
        {
            get => _valtSpel;
            set => SetProperty(ref _valtSpel, value);
        }

        public Spel? ValtSpelForRedigering
        {
            get => _valtSpelForRedigering;
            set
            {
                if (SetProperty(ref _valtSpelForRedigering, value))
                {
                    FyllSpelformularFranValtSpel();
                    UppdateraCommandStatus();
                }
            }
        }

        public SpelTraff? ValdTraff
        {
            get => _valdTraff;
            set
            {
                if (SetProperty(ref _valdTraff, value))
                {
                    UppdateraDeltagare();
                    UppdateraCommandStatus();
                }
            }
        }

        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        public string NyMedlemNamn
        {
            get => _nyMedlemNamn;
            set => SetProperty(ref _nyMedlemNamn, value);
        }

        public string NyMedlemEfternamn
        {
            get => _nyMedlemEfternamn;
            set => SetProperty(ref _nyMedlemEfternamn, value);
        }

        public string NyMedlemEpost
        {
            get => _nyMedlemEpost;
            set => SetProperty(ref _nyMedlemEpost, value);
        }

        public string NyMedlemMobilnummer
        {
            get => _nyMedlemMobilnummer;
            set => SetProperty(ref _nyMedlemMobilnummer, value);
        }

        public string NyMedlemRoll
        {
            get => _nyMedlemRoll;
            set => SetProperty(ref _nyMedlemRoll, value);
        }

        public string NyttSpelTitel
        {
            get => _nyttSpelTitel;
            set => SetProperty(ref _nyttSpelTitel, value);
        }

        public string NyttSpelKategori
        {
            get => _nyttSpelKategori;
            set => SetProperty(ref _nyttSpelKategori, value);
        }

        public string NyttSpelMin
        {
            get => _nyttSpelMin;
            set => SetProperty(ref _nyttSpelMin, value);
        }

        public string NyttSpelMax
        {
            get => _nyttSpelMax;
            set => SetProperty(ref _nyttSpelMax, value);
        }

        public string NyTraffNamn
        {
            get => _nyTraffNamn;
            set => SetProperty(ref _nyTraffNamn, value);
        }

        public string NyTraffPlats
        {
            get => _nyTraffPlats;
            set => SetProperty(ref _nyTraffPlats, value);
        }

        public DateTime? NyTraffDatum
        {
            get => _nyTraffDatum;
            set => SetProperty(ref _nyTraffDatum, value);
        }

        public string NyTraffTid
        {
            get => _nyTraffTid;
            set => SetProperty(ref _nyTraffTid, value);
        }

        public string NyTraffMin
        {
            get => _nyTraffMin;
            set => SetProperty(ref _nyTraffMin, value);
        }

        public string NyTraffMax
        {
            get => _nyTraffMax;
            set => SetProperty(ref _nyTraffMax, value);
        }

        public string GrupperingText
        {
            get => _grupperingText;
            set => SetProperty(ref _grupperingText, value);
        }

        public async Task LaddaAsync()
        {
            try
            {
                StatusText = "Laddar data...";
                await _service.StartaDatabasAsync();
                await LaddaMedlemmarAsync();
                await LaddaSpelAsync();
                await LaddaTraffarAsync(false);
                StatusText = "Data laddad från databasen.";
            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
            }
        }

        private async Task LaddaMedlemmarAsync()
        {
            var medlemmar = await _service.HamtaMedlemmarAsync();
            FyllSamling(Medlemmar, medlemmar);
        }

        private async Task LaddaSpelAsync()
        {
            var spel = await _service.HamtaSpelAsync();
            FyllSamling(Spel, spel);
        }

        private async Task LaddaTraffarAsync(bool baraLediga)
        {
            var traffar = await _service.HamtaSpeltraffarAsync(baraLediga);
            FyllSamling(Speltraffar, traffar);
            ValdTraff = null;
            StatusText = baraLediga ? "Visar spelträffar med lediga platser." : "Visar alla spelträffar.";
        }

        private async Task RegistreraMedlemAsync()
        {
            await HanteraFelAsync(async () =>
            {
                await _service.RegistreraMedlemAsync(NyMedlemNamn, NyMedlemEfternamn, NyMedlemEpost, NyMedlemMobilnummer, NyMedlemRoll);
                RensaMedlemsformular();
                await LaddaMedlemmarAsync();
                StatusText = "Ny medlem har registrerats.";
            });
        }

        private async Task UppdateraMedlemAsync()
        {
            if (ValdMedlem == null)
                return;

            await HanteraFelAsync(async () =>
            {
                var medlemId = ValdMedlem.Id;
                await _service.UppdateraMedlemAsync(medlemId, NyMedlemNamn, NyMedlemEfternamn, NyMedlemEpost, NyMedlemMobilnummer, NyMedlemRoll);
                await LaddaMedlemmarAsync();
                ValdMedlem = Medlemmar.FirstOrDefault(m => m.Id == medlemId);
                StatusText = "Medlemmen har uppdaterats.";
            });
        }

        private async Task AvregistreraMedlemAsync()
        {
            if (ValdMedlem == null)
                return;

            await HanteraFelAsync(async () =>
            {
                await _service.AvregistreraMedlemAsync(ValdMedlem.Id);
                ValdMedlem = null;
                await LaddaMedlemmarAsync();
                await LaddaTraffarAsync(false);
                StatusText = "Medlemmen har avregistrerats.";
            });
        }

        private async Task UppdateraMedlemStatusAsync(bool arSparrad)
        {
            if (ValdMedlem == null)
                return;

            await HanteraFelAsync(async () =>
            {
                await _service.UppdateraMedlemStatusAsync(ValdMedlem.Id, arSparrad);
                await LaddaMedlemmarAsync();
                await LaddaTraffarAsync(false);
                StatusText = arSparrad ? "Medlemmen har spärrats." : "Medlemmen har avspärrats.";
            });
        }

        private async Task VisaAktivaMedlemmarAsync()
        {
            var aktiva = (await _service.HamtaMedlemmarAsync())
                .Where(m => !m.ArSparrad)
                .OrderBy(m => m.Namn)
                .ThenBy(m => m.Efternamn)
                .ToList();

            FyllSamling(Medlemmar, aktiva);
            StatusText = "Visar aktiva medlemmar.";
        }

        private async Task SorteraMedlemmarAsync()
        {
            var sorterade = (await _service.HamtaMedlemmarAsync())
                .OrderBy(m => m.Namn)
                .ThenBy(m => m.Efternamn)
                .ToList();

            FyllSamling(Medlemmar, sorterade);
            StatusText = "Medlemmar sorterade A-Ö.";
        }

        private async Task RegistreraSpelAsync()
        {
            await HanteraFelAsync(async () =>
            {
                var min = LasHeltal(NyttSpelMin, "Min spelare");
                var max = LasHeltal(NyttSpelMax, "Max spelare");
                await _service.RegistreraSpelAsync(NyttSpelTitel, NyttSpelKategori, min, max);

                NyttSpelTitel = string.Empty;
                NyttSpelKategori = string.Empty;
                NyttSpelMin = string.Empty;
                NyttSpelMax = string.Empty;

                await LaddaSpelAsync();
                StatusText = "Nytt spel har registrerats.";
            });
        }

        private async Task UppdateraSpelAsync()
        {
            if (ValtSpelForRedigering == null)
                return;

            await HanteraFelAsync(async () =>
            {
                var min = LasHeltal(NyttSpelMin, "Min spelare");
                var max = LasHeltal(NyttSpelMax, "Max spelare");
                var spelId = ValtSpelForRedigering.Id;

                await _service.UppdateraSpelAsync(spelId, NyttSpelTitel, NyttSpelKategori, min, max);
                await LaddaSpelAsync();
                ValtSpelForRedigering = Spel.FirstOrDefault(s => s.Id == spelId);
                StatusText = "Spelet har uppdaterats.";
            });
        }

        private async Task RegistreraSpeltraffAsync()
        {
            await HanteraFelAsync(async () =>
            {
                if (ValtSpel == null)
                    throw new InvalidOperationException("Välj ett spel.");

                if (NyTraffDatum == null)
                    throw new InvalidOperationException("Välj datum för spelträffen.");

                if (!TimeSpan.TryParse(NyTraffTid, out var tid))
                    throw new InvalidOperationException("Tid måste skrivas korrekt, till exempel 18:00.");

                var datumTid = NyTraffDatum.Value.Date.Add(tid);

                if (datumTid <= DateTime.Now)
                    throw new InvalidOperationException("Spelträffen måste ligga i framtiden.");

                var min = LasHeltal(NyTraffMin, "Min deltagare");
                var max = LasHeltal(NyTraffMax, "Max deltagare");

                await _service.RegistreraSpeltraffAsync(NyTraffNamn, NyTraffPlats, min, max, datumTid, ValtSpel.Id);

                NyTraffNamn = string.Empty;
                NyTraffPlats = string.Empty;
                NyTraffDatum = DateTime.Today;
                NyTraffTid = "18:00";
                NyTraffMin = string.Empty;
                NyTraffMax = string.Empty;
                ValtSpel = null;

                await LaddaTraffarAsync(false);
                StatusText = "Ny spelträff har registrerats.";
            });
        }

        private async Task AnmalAsync()
        {
            if (ValdMedlem == null || ValdTraff == null)
                return;

            await HanteraFelAsync(async () =>
            {
                await _service.AnmalAsync(ValdMedlem.Id, ValdTraff.Id);
                await LaddaTraffarAsync(false);
                StatusText = "Medlemmen har anmälts.";
            });
        }

        private async Task AvanmalAsync()
        {
            if (ValdDeltagare == null || ValdTraff == null)
                return;

            await HanteraFelAsync(async () =>
            {
                await _service.AvanmalAsync(ValdDeltagare.Id, ValdTraff.Id);
                await LaddaTraffarAsync(false);
                StatusText = "Deltagaren har avanmälts.";
            });
        }

        private async Task TaBortSpeltraffAsync()
        {
            if (ValdTraff == null)
                return;

            await HanteraFelAsync(async () =>
            {
                await _service.TaBortSpeltraffAsync(ValdTraff.Id);
                await LaddaTraffarAsync(false);
                StatusText = "Spelträffen har tagits bort.";
            });
        }

        private void GrupperaSpel()
        {
            var grupper = Spel
                .GroupBy(s => s.Kategori)
                .OrderBy(g => g.Key);

            var sb = new StringBuilder();

            foreach (var grupp in grupper)
            {
                sb.AppendLine(grupp.Key);

                foreach (var spel in grupp.OrderBy(s => s.Titel))
                    sb.AppendLine($"- {spel.Titel}");

                sb.AppendLine();
            }

            GrupperingText = sb.ToString();
            StatusText = "Spel grupperade efter kategori.";
        }

        private void UppdateraDeltagare()
        {
            Deltagare.Clear();

            if (ValdTraff == null)
                return;

            foreach (var medlem in ValdTraff.Deltagare.OrderBy(m => m.Namn).ThenBy(m => m.Efternamn))
                Deltagare.Add(medlem);
        }

        private void UppdateraCommandStatus()
        {
            AvregistreraMedlemCommand.RaiseCanExecuteChanged();
            UppdateraMedlemCommand.RaiseCanExecuteChanged();
            UppdateraSpelCommand.RaiseCanExecuteChanged();
            SparraMedlemCommand.RaiseCanExecuteChanged();
            AvsparraMedlemCommand.RaiseCanExecuteChanged();
            AnmalCommand.RaiseCanExecuteChanged();
            AvanmalCommand.RaiseCanExecuteChanged();
            TaBortSpeltraffCommand.RaiseCanExecuteChanged();
        }

        private async Task HanteraFelAsync(Func<Task> action)
        {
            try
            {
                await action();
            }
            catch (Exception ex)
            {
                StatusText = ex.Message;
            }
        }

        private void FyllMedlemsformularFranValdMedlem()
        {
            if (ValdMedlem == null)
                return;

            NyMedlemNamn = ValdMedlem.Namn;
            NyMedlemEfternamn = ValdMedlem.Efternamn;
            NyMedlemEpost = ValdMedlem.Epost;
            NyMedlemMobilnummer = ValdMedlem.Mobilnummer;
            NyMedlemRoll = ValdMedlem.Roll;
        }

        private void RensaMedlemsformular()
        {
            NyMedlemNamn = string.Empty;
            NyMedlemEfternamn = string.Empty;
            NyMedlemEpost = string.Empty;
            NyMedlemMobilnummer = string.Empty;
            NyMedlemRoll = "Spelare";
        }

        private void FyllSpelformularFranValtSpel()
        {
            if (ValtSpelForRedigering == null)
                return;

            NyttSpelTitel = ValtSpelForRedigering.Titel;
            NyttSpelKategori = ValtSpelForRedigering.Kategori;
            NyttSpelMin = ValtSpelForRedigering.MinSpelare.ToString();
            NyttSpelMax = ValtSpelForRedigering.MaxSpelare.ToString();
        }

        private static int LasHeltal(string text, string faltNamn)
        {
            if (!int.TryParse(text, out var value))
                throw new InvalidOperationException($"{faltNamn} måste vara en siffra.");

            return value;
        }

        private static void FyllSamling<T>(ObservableCollection<T> samling, IEnumerable<T> varden)
        {
            samling.Clear();

            foreach (var varde in varden)
                samling.Add(varde);
        }
    }
}

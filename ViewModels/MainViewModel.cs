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
        private Spel? _valtSpel;
        private SpelTraff? _valdTraff;
        private string _statusText = "Redo.";
        private string _nyMedlemNamn = string.Empty;
        private string _nyMedlemEpost = string.Empty;
        private string _nyMedlemMobilnummer = string.Empty;
        private string _nyttSpelTitel = string.Empty;
        private string _nyttSpelKategori = string.Empty;
        private string _nyttSpelMin = string.Empty;
        private string _nyttSpelMax = string.Empty;
        private string _nyTraffNamn = string.Empty;
        private string _nyTraffPlats = string.Empty;
        private string _nyTraffDatumTid = DateTime.Today.AddHours(18).ToString("yyyy-MM-dd HH:mm");
        private string _nyTraffMin = string.Empty;
        private string _nyTraffMax = string.Empty;
        private string _grupperingText = string.Empty;

        public ObservableCollection<Medlem> Medlemmar { get; } = new();
        public ObservableCollection<Spel> Spel { get; } = new();
        public ObservableCollection<SpelTraff> Speltraffar { get; } = new();
        public ObservableCollection<Medlem> Deltagare { get; } = new();

        public MainViewModel()
        {
            LaddaCommand = new AsyncRelayCommand(LaddaAsync);
            RegistreraMedlemCommand = new AsyncRelayCommand(RegistreraMedlemAsync);
            AvregistreraMedlemCommand = new AsyncRelayCommand(AvregistreraMedlemAsync, () => ValdMedlem != null);
            SparraMedlemCommand = new AsyncRelayCommand(() => UppdateraMedlemStatusAsync(true), () => ValdMedlem != null);
            AvsparraMedlemCommand = new AsyncRelayCommand(() => UppdateraMedlemStatusAsync(false), () => ValdMedlem != null);
            VisaAktivaMedlemmarCommand = new AsyncRelayCommand(VisaAktivaMedlemmarAsync);
            SorteraMedlemmarCommand = new AsyncRelayCommand(SorteraMedlemmarAsync);
            RegistreraSpelCommand = new AsyncRelayCommand(RegistreraSpelAsync);
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
        public AsyncRelayCommand AvregistreraMedlemCommand { get; }
        public AsyncRelayCommand SparraMedlemCommand { get; }
        public AsyncRelayCommand AvsparraMedlemCommand { get; }
        public AsyncRelayCommand VisaAktivaMedlemmarCommand { get; }
        public AsyncRelayCommand SorteraMedlemmarCommand { get; }
        public AsyncRelayCommand RegistreraSpelCommand { get; }
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
                    UppdateraCommandStatus();
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

        public string NyTraffDatumTid
        {
            get => _nyTraffDatumTid;
            set => SetProperty(ref _nyTraffDatumTid, value);
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
                await _service.RegistreraMedlemAsync(NyMedlemNamn, NyMedlemEpost, NyMedlemMobilnummer);
                NyMedlemNamn = string.Empty;
                NyMedlemEpost = string.Empty;
                NyMedlemMobilnummer = string.Empty;
                await LaddaMedlemmarAsync();
                StatusText = "Ny medlem har registrerats.";
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
                .ToList();

            FyllSamling(Medlemmar, aktiva);
            StatusText = "Visar aktiva medlemmar.";
        }

        private async Task SorteraMedlemmarAsync()
        {
            var sorterade = (await _service.HamtaMedlemmarAsync())
                .OrderBy(m => m.Namn)
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

        private async Task RegistreraSpeltraffAsync()
        {
            await HanteraFelAsync(async () =>
            {
                if (ValtSpel == null)
                    throw new InvalidOperationException("Välj ett spel.");

                if (!DateTime.TryParse(NyTraffDatumTid, out var datumTid))
                    throw new InvalidOperationException("Datum och tid måste skrivas korrekt.");

                var min = LasHeltal(NyTraffMin, "Min deltagare");
                var max = LasHeltal(NyTraffMax, "Max deltagare");

                await _service.RegistreraSpeltraffAsync(NyTraffNamn, NyTraffPlats, min, max, datumTid, ValtSpel.Id);

                NyTraffNamn = string.Empty;
                NyTraffPlats = string.Empty;
                NyTraffDatumTid = DateTime.Today.AddHours(18).ToString("yyyy-MM-dd HH:mm");
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

            foreach (var medlem in ValdTraff.Deltagare.OrderBy(m => m.Namn))
                Deltagare.Add(medlem);
        }

        private void UppdateraCommandStatus()
        {
            AvregistreraMedlemCommand.RaiseCanExecuteChanged();
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

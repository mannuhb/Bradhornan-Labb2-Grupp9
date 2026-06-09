using System;
using System.Collections.Generic;

namespace ObjektLab1.Models
{
    public class SpelTraff
    {
        public int Id { get; private set; }
        public string Namn { get; private set; } = string.Empty;
        public string Plats { get; private set; } = string.Empty;
        public DateTime DatumTid { get; private set; }
        public int MinDeltagare { get; private set; }
        public int MaxDeltagare { get; private set; }
        public int SpeltidMinuter { get; private set; }
        public int SpelId { get; private set; }
        public Spel Spel { get; private set; } = null!;
        public List<Medlem> Deltagare { get; private set; } = new();

        public int LedigaPlatser
        {
            get { return MaxDeltagare - Deltagare.Count; }
        }

        public SpelTraff()
        {
        }

        public SpelTraff(string namn, string plats, int minDeltagare, int maxDeltagare, DateTime datumTid, Spel spel)
        {
            if (string.IsNullOrWhiteSpace(namn))
                throw new ArgumentException("Namn på spelträff måste anges.");

            if (string.IsNullOrWhiteSpace(plats))
                throw new ArgumentException("Plats måste anges.");

            if (datumTid == default)
                throw new ArgumentException("Datum och tid måste anges.");

            if (minDeltagare <= 0)
                throw new ArgumentException("Minsta antal deltagare måste vara större än 0.");

            if (maxDeltagare < minDeltagare)
                throw new ArgumentException("Max deltagare kan inte vara mindre än min deltagare.");

            if (spel == null)
                throw new ArgumentException("Spel måste väljas.");

            if (minDeltagare < spel.MinSpelare || maxDeltagare > spel.MaxSpelare)
                throw new Exception($"Spelet {spel.Titel} passar inte för träffen. Spelet kräver {spel.MinSpelare}-{spel.MaxSpelare} spelare.");

            Namn = namn;
            Plats = plats;
            MinDeltagare = minDeltagare;
            MaxDeltagare = maxDeltagare;
            DatumTid = datumTid;
            Spel = spel;
            SpeltidMinuter = 60;
        }

        public DateTime SlutTid()
        {
            return DatumTid.AddMinutes(SpeltidMinuter);
        }

        public void Anmal(Medlem medlem)
        {
            if (medlem == null)
                throw new Exception("Välj en medlem först.");

            if (!medlem.KanAnmalasTillTraff())
                throw new Exception("Medlemmen är spärrad och kan inte anmäla sig.");

            if (Deltagare.Contains(medlem))
                throw new Exception("Denna medlem är redan anmäld till träffen.");

            if (Deltagare.Count >= MaxDeltagare)
                throw new Exception("Spelträffen är full. Det går inte att anmäla fler.");

            Deltagare.Add(medlem);
        }

        public void Avanmal(Medlem medlem)
        {
            if (medlem == null)
                throw new Exception("Välj en medlem först.");

            if (!Deltagare.Contains(medlem))
                throw new Exception("Medlemmen är inte anmäld till träffen.");

            Deltagare.Remove(medlem);
        }

        public override string ToString()
        {
            return $"ID {Id} - {Namn} - {Spel.Titel} - {Plats} - {DatumTid:yyyy-MM-dd HH:mm}-{SlutTid():HH:mm} - krav {MinDeltagare}-{MaxDeltagare} deltagare - {LedigaPlatser} lediga platser ({Deltagare.Count}/{MaxDeltagare})";
        }
    }
}

using System;
using System.Collections.Generic;

namespace ObjektLab1.Models
{
    public class Medlem
    {
        public int Id { get; private set; }
        public int Medlemsnummer { get; private set; }
        public string Namn { get; private set; } = string.Empty;
        public string Epost { get; private set; } = string.Empty;
        public string Mobilnummer { get; private set; } = string.Empty;
        public string Roll { get; private set; } = "Spelare";
        public bool ArSparrad { get; private set; }
        public List<SpelTraff> Speltraffar { get; private set; } = new();

        protected Medlem()
        {
        }

        public Medlem(int medlemsnummer, string namn, string epost, string mobilnummer, string roll)
        {
            ValideraMedlemsnummer(medlemsnummer);
            ValideraNamn(namn);
            ValideraEpost(epost);
            ValideraMobilnummer(mobilnummer);
            ValideraRoll(roll);

            Medlemsnummer = medlemsnummer;
            Namn = namn.Trim();
            Epost = epost.Trim();
            Mobilnummer = mobilnummer.Trim();
            Roll = roll.Trim();
            ArSparrad = false;
        }

        public bool KanAnmalasTillTraff()
        {
            return !ArSparrad;
        }

        public void UppdateraKontaktuppgifter(string namn, string epost, string mobilnummer, string roll)
        {
            ValideraNamn(namn);
            ValideraEpost(epost);
            ValideraMobilnummer(mobilnummer);
            ValideraRoll(roll);

            Namn = namn.Trim();
            Epost = epost.Trim();
            Mobilnummer = mobilnummer.Trim();
            Roll = roll.Trim();
        }

        public void Sparra()
        {
            ArSparrad = true;
        }

        public void Avsparra()
        {
            ArSparrad = false;
        }

        public override string ToString()
        {
            var status = ArSparrad ? " (SPÄRRAD)" : string.Empty;
            return $"{Medlemsnummer} - {Namn} - {Epost} - {Mobilnummer} - {Roll}{status}";
        }

        private static void ValideraMedlemsnummer(int medlemsnummer)
        {
            if (medlemsnummer <= 0)
                throw new ArgumentException("Medlemsnummer måste vara större än 0.");
        }

        private static void ValideraNamn(string namn)
        {
            if (string.IsNullOrWhiteSpace(namn))
                throw new ArgumentException("Namn måste anges.");
        }

        private static void ValideraEpost(string epost)
        {
            if (string.IsNullOrWhiteSpace(epost) || !epost.Contains('@'))
                throw new ArgumentException("E-post måste anges i giltigt format.");
        }

        private static void ValideraMobilnummer(string mobilnummer)
        {
            if (string.IsNullOrWhiteSpace(mobilnummer))
                throw new ArgumentException("Mobilnummer måste anges.");

            foreach (var tecken in mobilnummer)
            {
                if (!char.IsDigit(tecken) && tecken != ' ' && tecken != '-' && tecken != '+')
                    throw new ArgumentException("Mobilnummer får bara innehålla siffror, mellanslag, bindestreck och plus.");
            }
        }

        private static void ValideraRoll(string roll)
        {
            if (string.IsNullOrWhiteSpace(roll))
                throw new ArgumentException("Roll måste anges.");
        }
    }
}

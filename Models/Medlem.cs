using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ObjektLab1.Models
{
    public class Medlem
    {
        public int Id { get; private set; }
        [NotMapped]
        public int Medlemsnummer => Id;
        public string Namn { get; private set; } = string.Empty;
        public string Efternamn { get; private set; } = string.Empty;
        public string Epost { get; private set; } = string.Empty;
        public string Mobilnummer { get; private set; } = string.Empty;
        public string Roll { get; private set; } = "Spelare";
        public bool ArSparrad { get; private set; }
        public List<SpelTraff> Speltraffar { get; private set; } = new();

        protected Medlem()
        {
        }

        public Medlem(int medlemsnummer, string namn, string efternamn, string epost, string mobilnummer, string roll)
        {
            ValideraMedlemsnummer(medlemsnummer);
            ValideraNamn(namn);
            ValideraEfternamn(efternamn);
            ValideraEpost(epost);
            ValideraMobilnummer(mobilnummer);
            ValideraRoll(roll);

            Id = medlemsnummer;
            Namn = namn.Trim();
            Efternamn = efternamn.Trim();
            Epost = epost.Trim();
            Mobilnummer = mobilnummer.Trim();
            Roll = roll.Trim();
            ArSparrad = false;
        }

        public bool KanAnmalasTillTraff()
        {
            return !ArSparrad;
        }

        public void UppdateraKontaktuppgifter(string namn, string efternamn, string epost, string mobilnummer, string roll)
        {
            ValideraNamn(namn);
            ValideraEfternamn(efternamn);
            ValideraEpost(epost);
            ValideraMobilnummer(mobilnummer);
            ValideraRoll(roll);

            Namn = namn.Trim();
            Efternamn = efternamn.Trim();
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
            return $"ID {Id} - {Namn} {Efternamn} - {Epost} - {Mobilnummer} - {Roll}{status}";
        }

        private static void ValideraMedlemsnummer(int medlemsnummer)
        {
            if (medlemsnummer <= 0)
                throw new ArgumentException("Medlemsnummer måste vara större än 0.");
        }

        private static void ValideraNamn(string namn)
        {
            if (string.IsNullOrWhiteSpace(namn))
                throw new ArgumentException("Förnamn måste anges.");

            if (!InnehallerBaraBokstaverOchBindestreck(namn))
                throw new ArgumentException("Förnamn får bara innehålla bokstäver, mellanslag och bindestreck.");
        }

        private static void ValideraEfternamn(string efternamn)
        {
            if (string.IsNullOrWhiteSpace(efternamn))
                throw new ArgumentException("Efternamn måste anges.");

            if (!InnehallerBaraBokstaverOchBindestreck(efternamn))
                throw new ArgumentException("Efternamn får bara innehålla bokstäver, mellanslag och bindestreck.");
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

            if (mobilnummer.Length != 10)
                throw new ArgumentException("Mobilnummer måste vara exakt 10 siffror.");

            if (!mobilnummer.StartsWith('0'))
                throw new ArgumentException("Mobilnummer måste börja med 0.");

            if (!mobilnummer.All(char.IsDigit))
                throw new ArgumentException("Mobilnummer får bara innehålla siffror.");
        }

        private static void ValideraRoll(string roll)
        {
            if (string.IsNullOrWhiteSpace(roll))
                throw new ArgumentException("Roll måste anges.");
        }

        private static bool InnehallerBaraBokstaverOchBindestreck(string text)
        {
            foreach (var tecken in text)
            {
                if (!char.IsLetter(tecken) && tecken != ' ' && tecken != '-')
                    return false;
            }

            return true;
        }
    }
}

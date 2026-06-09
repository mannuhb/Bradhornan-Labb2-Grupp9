using System;

using System.Collections.Generic;

namespace ObjektLab1.Models
{
    public class Spel
    {
        public int Id { get; private set; }
        public string Titel { get; private set; } = string.Empty;
        public string Kategori { get; private set; } = string.Empty;
        public int MinSpelare { get; private set; }
        public int MaxSpelare { get; private set; }
        public List<SpelTraff> Speltraffar { get; private set; } = new();

        public Spel()
        {
        }

        public Spel(string titel, string kategori, int minSpelare, int maxSpelare)
        {
            Validera(titel, kategori, minSpelare, maxSpelare);

            Titel = titel.Trim();
            Kategori = kategori.Trim();
            MinSpelare = minSpelare;
            MaxSpelare = maxSpelare;
        }

        public void Uppdatera(string titel, string kategori, int minSpelare, int maxSpelare)
        {
            Validera(titel, kategori, minSpelare, maxSpelare);

            Titel = titel.Trim();
            Kategori = kategori.Trim();
            MinSpelare = minSpelare;
            MaxSpelare = maxSpelare;
        }

        private static void Validera(string titel, string kategori, int minSpelare, int maxSpelare)
        {
            if (string.IsNullOrWhiteSpace(titel))
                throw new ArgumentException("Titel måste anges.");

            if (string.IsNullOrWhiteSpace(kategori))
                throw new ArgumentException("Kategori måste anges.");

            if (minSpelare <= 0 || maxSpelare < minSpelare)
                throw new ArgumentException("Fel antal spelare.");
        }

        public override string ToString()
        {
            return $"{Titel} - {Kategori} ({MinSpelare}-{MaxSpelare} spelare)";
        }
    }
}

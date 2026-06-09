using System;
using System.Collections.Generic;
using System.Linq;

namespace ObjektLab1.Models
{
    public class Forening
    {
        public List<Medlem> Medlemmar { get; private set; }
        public List<Spel> Spel { get; private set; }
        public List<SpelTraff> Speltraffar { get; private set; }

        public Forening()
        {
            Medlemmar = new List<Medlem>();
            Spel = new List<Spel>();
            Speltraffar = new List<SpelTraff>();
        }

        public void AvregistreraMedlem(Medlem medlem)
        {
            if (medlem == null)
                throw new Exception("Välj en medlem först.");

            Medlemmar.Remove(medlem);

            foreach (var traff in Speltraffar)
            {
                if (traff.Deltagare.Contains(medlem))
                    traff.Deltagare.Remove(medlem);
            }
        }

        public void SparraMedlem(Medlem medlem)
        {
            if (medlem == null)
                throw new Exception("Välj en medlem först.");

            medlem.Sparra();
        }

        public void AvsparraMedlem(Medlem medlem)
        {
            if (medlem == null)
                throw new Exception("Välj en medlem först.");

            medlem.Avsparra();
        }

        public List<Medlem> HamtaAktivaMedlemmar()
        {
            return Medlemmar
                .Where(m => !m.ArSparrad)
                .ToList();
        }

        public List<Medlem> SorteraMedlemmarEfterNamn()
        {
            return Medlemmar
                .OrderBy(m => m.Namn)
                .ToList();
        }

        public List<SpelTraff> HamtaTraffarMedLedigaPlatser()
        {
            return Speltraffar
                .Where(t => t.LedigaPlatser > 0)
                .OrderBy(t => t.DatumTid)
                .ToList();
        }

        public List<SpelTraff> HamtaAllaTraffarSorterade()
        {
            return Speltraffar
                .OrderBy(t => t.DatumTid)
                .ToList();
        }

        public IEnumerable<IGrouping<string, Spel>> GrupperaSpelEfterKategori()
        {
            return Spel
                .GroupBy(s => s.Kategori);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ObjektLab1.Data;
using ObjektLab1.Models;

namespace ObjektLab1.Services
{
    public class ForeningService
    {
        public async Task StartaDatabasAsync()
        {
            using var db = new ForeningDbContext();
            await db.Database.EnsureCreatedAsync();

            if (await db.Medlemmar.AnyAsync())
                return;

            var demo = DemoData.SkapaDemoForening();

            db.Medlemmar.AddRange(demo.Medlemmar);
            db.Spel.AddRange(demo.Spel);
            db.Speltraffar.AddRange(demo.Speltraffar);

            await db.SaveChangesAsync();
        }

        public async Task<List<Medlem>> HamtaMedlemmarAsync()
        {
            using var db = new ForeningDbContext();
            return await db.Medlemmar
                .OrderBy(m => m.Medlemsnummer)
                .ToListAsync();
        }

        public async Task<List<Spel>> HamtaSpelAsync()
        {
            using var db = new ForeningDbContext();
            return await db.Spel
                .OrderBy(s => s.Kategori)
                .ThenBy(s => s.Titel)
                .ToListAsync();
        }

        public async Task<List<SpelTraff>> HamtaSpeltraffarAsync(bool baraLediga = false)
        {
            using var db = new ForeningDbContext();
            var query = db.Speltraffar
                .Include(t => t.Spel)
                .Include(t => t.Deltagare)
                .AsQueryable();

            if (baraLediga)
                query = query.Where(t => t.Deltagare.Count < t.MaxDeltagare);

            return await query
                .OrderBy(t => t.DatumTid)
                .ToListAsync();
        }

        public async Task<Medlem> RegistreraMedlemAsync(string namn, string epost, string mobilnummer, string roll)
        {
            using var db = new ForeningDbContext();
            epost = epost.Trim();
            mobilnummer = mobilnummer.Trim();

            if (await db.Medlemmar.AnyAsync(m => m.Epost == epost))
                throw new InvalidOperationException("Det finns redan en medlem med samma e-postadress.");

            if (await db.Medlemmar.AnyAsync(m => m.Mobilnummer == mobilnummer))
                throw new InvalidOperationException("Det finns redan en medlem med samma mobilnummer.");

            var nyttNummer = await db.Medlemmar.AnyAsync()
                ? await db.Medlemmar.MaxAsync(m => m.Medlemsnummer) + 1
                : 1;

            var medlem = new Medlem(nyttNummer, namn, epost, mobilnummer, roll);
            db.Medlemmar.Add(medlem);
            await db.SaveChangesAsync();
            return medlem;
        }

        public async Task<Spel> RegistreraSpelAsync(string titel, string kategori, int minSpelare, int maxSpelare)
        {
            using var db = new ForeningDbContext();
            var spel = new Spel(titel, kategori, minSpelare, maxSpelare);
            db.Spel.Add(spel);
            await db.SaveChangesAsync();
            return spel;
        }

        public async Task<SpelTraff> RegistreraSpeltraffAsync(string namn, string plats, int minDeltagare, int maxDeltagare, DateTime datumTid, int spelId)
        {
            using var db = new ForeningDbContext();
            var spel = await db.Spel.FindAsync(spelId)
                ?? throw new InvalidOperationException("Välj ett spel.");

            var traff = new SpelTraff(namn, plats, minDeltagare, maxDeltagare, datumTid, spel);
            db.Speltraffar.Add(traff);
            await db.SaveChangesAsync();
            return traff;
        }

        public async Task AnmalAsync(int medlemId, int traffId)
        {
            using var db = new ForeningDbContext();
            var traff = await db.Speltraffar
                .Include(t => t.Spel)
                .Include(t => t.Deltagare)
                .FirstOrDefaultAsync(t => t.Id == traffId)
                ?? throw new InvalidOperationException("Välj en spelträff.");

            var medlem = await db.Medlemmar.FindAsync(medlemId)
                ?? throw new InvalidOperationException("Välj en medlem.");

            traff.Anmal(medlem);
            await db.SaveChangesAsync();
        }

        public async Task AvanmalAsync(int medlemId, int traffId)
        {
            using var db = new ForeningDbContext();
            var traff = await db.Speltraffar
                .Include(t => t.Deltagare)
                .FirstOrDefaultAsync(t => t.Id == traffId)
                ?? throw new InvalidOperationException("Välj en spelträff.");

            var medlem = traff.Deltagare.FirstOrDefault(m => m.Id == medlemId)
                ?? throw new InvalidOperationException("Medlemmen är inte anmäld till träffen.");

            traff.Avanmal(medlem);
            await db.SaveChangesAsync();
        }

        public async Task UppdateraMedlemStatusAsync(int medlemId, bool arSparrad)
        {
            using var db = new ForeningDbContext();
            var medlem = await db.Medlemmar.FindAsync(medlemId)
                ?? throw new InvalidOperationException("Välj en medlem.");

            if (arSparrad)
                medlem.Sparra();
            else
                medlem.Avsparra();

            await db.SaveChangesAsync();
        }

        public async Task AvregistreraMedlemAsync(int medlemId)
        {
            using var db = new ForeningDbContext();
            var medlem = await db.Medlemmar
                .Include(m => m.Speltraffar)
                .FirstOrDefaultAsync(m => m.Id == medlemId)
                ?? throw new InvalidOperationException("Välj en medlem.");

            db.Medlemmar.Remove(medlem);
            await db.SaveChangesAsync();
        }

        public async Task TaBortSpeltraffAsync(int traffId)
        {
            using var db = new ForeningDbContext();
            var traff = await db.Speltraffar.FindAsync(traffId)
                ?? throw new InvalidOperationException("Välj en spelträff.");

            db.Speltraffar.Remove(traff);
            await db.SaveChangesAsync();
        }
    }
}

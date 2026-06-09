using Microsoft.EntityFrameworkCore;
using ObjektLab1.Models;

namespace ObjektLab1.Data
{
    public class ForeningDbContext : DbContext
    {
        private const string ConnectionString =
            @"Server=(localdb)\MSSQLLocalDB;Database=BradhornanDb;Trusted_Connection=True;";

        public DbSet<Medlem> Medlemmar => Set<Medlem>();
        public DbSet<Spel> Spel => Set<Spel>();
        public DbSet<SpelTraff> Speltraffar => Set<SpelTraff>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Medlem>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Medlem>()
                .Property(m => m.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Medlem>()
                .HasIndex(m => m.Epost)
                .IsUnique();

            modelBuilder.Entity<Medlem>()
                .HasAlternateKey(m => m.Epost);

            modelBuilder.Entity<Medlem>()
                .HasIndex(m => m.Mobilnummer)
                .IsUnique();

            modelBuilder.Entity<Spel>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<SpelTraff>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<SpelTraff>()
                .HasOne(t => t.Spel)
                .WithMany(s => s.Speltraffar)
                .HasForeignKey(t => t.SpelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SpelTraff>()
                .HasMany(t => t.Deltagare)
                .WithMany(m => m.Speltraffar);
        }
    }
}

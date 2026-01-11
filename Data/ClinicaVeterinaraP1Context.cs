using ClinicaVeterinaraP1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClinicaVeterinaraP1.Data
{
    public class ClinicaVeterinaraP1Context : IdentityDbContext
    {
        public ClinicaVeterinaraP1Context(DbContextOptions<ClinicaVeterinaraP1Context> options)
            : base(options)
        {
        }

        public DbSet<ClinicaVeterinaraP1.Models.Proprietar> Proprietar { get; set; } = default!;
        public DbSet<ClinicaVeterinaraP1.Models.Animal> Animal { get; set; } = default!;
        public DbSet<ClinicaVeterinaraP1.Models.MedicVeterinar> MedicVeterinar { get; set; } = default!;
        public DbSet<ClinicaVeterinaraP1.Models.Programare> Programare { get; set; } = default!;
        public DbSet<ClinicaVeterinaraP1.Models.Recenzie> Recenzie { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recenzie>()
                .HasOne(r => r.Programare)
                .WithOne(p => p.Recenzie)
                .HasForeignKey<Recenzie>(r => r.ProgramareId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
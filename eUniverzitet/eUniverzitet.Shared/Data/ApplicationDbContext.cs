using eUniverzitet.Shared.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace eUniverzitet.Shared.Data
{
    public class ApplicationDbContext : IdentityDbContext  <Korisnik>
    {
        public DbSet<LogKretanjePoSistemu> LogKretanjePoSistemu{ get; set; }
        public DbSet<Opstina> Opstina { get; set; }
        public DbSet<Korisnik> Korisnik{ get; set; }
        public DbSet<Student> Student { get; set; }

        public DbSet<Ocjene> Ocjene { get; set; }
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<PrisustvoNaNastavi> PrisustvoNaNastavi { get; set; }
        public DbSet<Nastavnik> Nastavnik { get; set; }
        public DbSet<Notifikacije> Notifikacije { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
               .SelectMany(t => t.GetForeignKeys())
               .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;


            base.OnModelCreating(modelBuilder);

            //one-to-one veza
            modelBuilder.Entity<Korisnik>()
                .HasOne<Student>(s => s.Student)
                .WithOne(ad => ad.Korisnik)
                .HasForeignKey<Student>(ad => ad.KorisnikID)
                ;

            modelBuilder.Entity<Korisnik>()
                .HasOne<Nastavnik>(s => s.Nastavnik)
                .WithOne(ad => ad.Korisnik)
                .HasForeignKey<Nastavnik>(ad => ad.KorisnikID)
                ;
        }
    }
}

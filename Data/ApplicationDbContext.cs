using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Spor_web_sitesi.Identity;
using Spor_web_sitesi.Models;

namespace Spor_web_sitesi.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Antrenor> Antrenorler { get; set; }
        public DbSet<Hizmet> Hizmetler { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<AntrenorMusaitlik> AntrenorMusaitlikler { get; set; }
        public DbSet<Salon> Salonlar { get; set; }
        public DbSet<Analiz> Analizler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // SALON SEED
            modelBuilder.Entity<Salon>().HasData(new Salon
            {
                Id = 1,
                Ad = "Fit-Pro Spor Salonu",
                Adres = "Sakarya Üniversitesi Kampüsü",
                Telefon = "0264 123 45 67",
                CalismaSaatleri = "09:00 - 22:00"
            });

            // =========================
            // ADMIN ROLE
            // =========================
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            });

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "2",
                Name = "User",
                NormalizedName = "USER"
            });

            var hasher = new PasswordHasher<AppUser>();

            var adminUser = new AppUser
            {
                Id = "1",
                UserName = "b231210086@sakarya.edu.tr",
                NormalizedUserName = "B231210086@SAKARYA.EDU.TR",
                Email = "b231210086@sakarya.edu.tr",
                NormalizedEmail = "B231210086@SAKARYA.EDU.TR",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "sau");

            modelBuilder.Entity<AppUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "1",
                    RoleId = "1"
                }
            );
        }
    }
}
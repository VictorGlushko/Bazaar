using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Bazaar.Domain.Entities;
using Bazaar.EntityConfigurations;

namespace Bazaar.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<MediaResources> MediaResources { get; set; }
        public DbSet<SystemRequirements> SystemRequirements { get; set; }
        public DbSet<FaqItem> FaqItems { get; set; }

        public DbSet<CarouselSlide> CarouselSlides { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GameConfiguration());

            modelBuilder.Configurations.Add(new GenreConfiguration());
            

            //modelBuilder.Configurations.Add(new PatientStatusConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
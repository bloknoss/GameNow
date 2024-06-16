using GameNow.Database.Mappings;
using GameNow.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameNow.Database
{
    public class GameNowContext : IdentityDbContext
    {
        public GameNowContext(DbContextOptions<GameNowContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Bundle> Bundles { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Specifications> Specifications { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new GameMapping());
            modelBuilder.ApplyConfiguration(new BundleMapping());
            modelBuilder.ApplyConfiguration(new DeveloperMapping());
            modelBuilder.ApplyConfiguration(new LanguageMapping());
            modelBuilder.ApplyConfiguration(new PaymentMapping());
            modelBuilder.ApplyConfiguration(new PublisherMapping());
            modelBuilder.ApplyConfiguration(new ReviewMapping());
            modelBuilder.ApplyConfiguration(new SpecificationMapping());
            modelBuilder.ApplyConfiguration(new StatusMapping());
            modelBuilder.ApplyConfiguration(new TagMapping());
        }
    }


}
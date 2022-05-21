using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Interfaces.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public DbSet<ComicBook> ComicBooks { get; set; }
        public DbSet<Screenwriter> Screenwriters { get; set; }
        public DbSet<Translator> Translators { get; set; }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<HeroesTeams> HeroesTeams { get; set; }
        public DbSet<Illustrator> Illustrators { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Poster> Posters { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ComicBookIllustrator> ComicBooksIllustrators { get; set; }
        public DbSet<ComicBookHeroesTeams> ComicBooksHeroesTeams { get; set; }
        public DbSet<Rating> Rating { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Client",
                    NormalizedName = "CLIENT"
                },
                new IdentityRole
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                });

            builder.Entity<ComicBookHeroesTeams>()
                .HasKey(ch => new { ch.ComicBookId, ch.HeroesTeamsId });

            builder.Entity<ComicBookHeroesTeams>()
                .HasOne(ch => ch.ComicBook)
                .WithMany(c => c.ComicBookHeroesTeams)
                .HasForeignKey(ch => ch.ComicBookId);

            builder.Entity<ComicBookHeroesTeams>()
                .HasOne(ch => ch.HeroesTeams)
                .WithMany(h => h.ComicBookHeroesTeams)
                .HasForeignKey(ch => ch.HeroesTeamsId);


            builder.Entity<ComicBookIllustrator>()
                .HasKey(ci => new { ci.ComicBookId, ci.IllustratorId });

            builder.Entity<ComicBookIllustrator>()
                .HasOne(ci => ci.ComicBook)
                .WithMany(c => c.ComicBookIllustrators)
                .HasForeignKey(ci => ci.ComicBookId);

            builder.Entity<ComicBookIllustrator>()
                .HasOne(ci => ci.Illustrator)
                .WithMany(i => i.ComicBookIllustrators)
                .HasForeignKey(ci => ci.IllustratorId);
        }
    }
}

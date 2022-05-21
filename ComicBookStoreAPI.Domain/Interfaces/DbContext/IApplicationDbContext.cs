using ComicBookStoreAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Interfaces.DbContext
{
    public interface IApplicationDbContext
    {
        DbSet<ComicBook> ComicBooks { get; set; }
        DbSet<ComicBookHeroesTeams> ComicBooksHeroesTeams { get; set; }
        DbSet<ComicBookIllustrator> ComicBooksIllustrators { get; set; }
        DbSet<Contact> Contacts { get; set; }
        DbSet<CoverType> CoverTypes { get; set; }
        DbSet<HeroesTeams> HeroesTeams { get; set; }
        DbSet<Illustrator> Illustrators { get; set; }
        DbSet<Poster> Posters { get; set; }
        DbSet<Screenwriter> Screenwriters { get; set; }
        DbSet<Series> Series { get; set; }
        DbSet<Translator> Translators { get; set; }
        public DbSet<Rating> Rating { get; set; }
    }
}


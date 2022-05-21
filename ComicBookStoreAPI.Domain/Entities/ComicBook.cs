using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace ComicBookStoreAPI.Domain.Entities
{
    public class ComicBook : IAlikeable<ComicBook>, IEntityWithId
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public string Edition { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfPages { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public int ScreenwriterId { get; set; }
        public virtual Screenwriter Screenwriter { get; set; }
        public int TranslatorId { get; set; }
        public virtual Translator Translator { get; set; }
        public int SeriesId { get; set; }
        public virtual Series Series { get; set; }
        public int CoverTypeId { get; set; }
        public virtual CoverType CoverType { get; set; }
        public int? Discount { get; set; }
        public virtual List<Poster> Posters { get; set; }
        public string Description { get; set; }
        public List<ComicBookIllustrator> ComicBookIllustrators { get; set; }
        public List<ComicBookHeroesTeams> ComicBookHeroesTeams { get; set; }

        public bool IsAlik(ComicBook entity)
        {
            return entity != null && this.Title == entity.Title && this.GetType() == entity.GetType() &&
                this.Price == entity.Price && this.Edition == entity.Edition &&
                this.ReleaseDate == entity.ReleaseDate && this.NumberOfPages == entity.NumberOfPages &&
                this.Ratings == entity.Ratings && this.CoverType == entity.CoverType &&
                this.Discount == entity.Discount && this.Screenwriter == entity.Screenwriter &&
                this.Translator == entity.Translator && this.Series == entity.Series &&
                this.Posters == entity.Posters && this.Description == entity.Description &&
                this.ComicBookIllustrators == entity.ComicBookIllustrators &&
                this.ComicBookHeroesTeams == entity.ComicBookHeroesTeams;
        }
    }
}

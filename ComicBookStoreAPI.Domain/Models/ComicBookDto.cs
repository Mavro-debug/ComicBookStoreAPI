using ComicBookStoreAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Models
{
    public class ComicBookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal Price { get; set; }
        public string Edition { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int NumberOfPages { get; set; }
        public List<RatingDto> Ratings { get; set; }
        public Screenwriter Screenwriter { get; set; }
        public Translator Translator { get; set; }
        public Series Series { get; set; }
        public int CoverTypeId { get; set; }
        public CoverType CoverType { get; set; }
        public int? Discount { get; set; }
        public List<PosterDto> Posters { get; set; }
        public string Description { get; set; }
        public List<ComicBookIllustrator> ComicBookIllustrators { get; set; }
        public List<ComicBookHeroesTeams> ComicBookHeroesTeams { get; set; }


    }
}

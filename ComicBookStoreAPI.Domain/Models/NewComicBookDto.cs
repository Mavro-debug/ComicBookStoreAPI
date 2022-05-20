using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Models
{
    public class NewComicBookDto
    {
        public string Tytle { get; set; }
        public decimal Price { get; set; }
        public string Edytion { get; set; }
        public DateDto ReleaseDate { get; set; }
        public int NumberOfPages { get; set; }
        public string Screenriter { get; set; }
        public string Translator { get; set; }
        public string Series { get; set; }
        public string CoverType { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public List<string> Illustrators { get; set; }
        public List<string> HeroesTeams { get; set; }
    }
}

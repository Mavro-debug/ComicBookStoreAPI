using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Models
{
    public class ComicBookDescFeatDto
    {
        public List<string> Screenwriters { get; set; }
        public List<string> Translators { get; set; }
        public List<string> Series { get; set; }
        public List<string> CoverTypes { get; set; }
        public List<string> Illustrators { get; set; }
        public List<string> HeroesTeams { get; set; }
    }
}

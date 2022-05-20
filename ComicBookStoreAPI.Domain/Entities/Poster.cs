using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class Poster
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool Cover { get; set; }
        public int ComicBookId { get; set; }
        public virtual ComicBook ComicBook { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Models
{
    public class PosterDto
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public bool Cover { get; set; }
    }
}

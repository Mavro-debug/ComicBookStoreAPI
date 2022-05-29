using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Models
{
    public class CreateRatingDto
    {
        public int Grade { get; set; }
        public string Commentary { get; set; }
    }
}

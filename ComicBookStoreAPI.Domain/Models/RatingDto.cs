using System.ComponentModel.DataAnnotations;

namespace ComicBookStoreAPI.Domain.Models
{
    public class RatingDto
    {
        public int Id { get; set; }
        [Range(0, 5)]
        public int Grade { get; set; }
        public string Commentary { get; set; }
    }
}

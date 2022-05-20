

using System.ComponentModel.DataAnnotations;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public virtual ComicBook ComicBook { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Commentary { get; set; }

        [Range(0, 5)]
        public int Grade { get; set; }
    }
}

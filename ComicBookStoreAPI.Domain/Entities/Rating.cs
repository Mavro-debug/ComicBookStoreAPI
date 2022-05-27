using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class Rating : IAlikeable<Rating>, IEntityWithId
    {
        public int Id { get; set; }
        public virtual ComicBook ComicBook { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string Commentary { get; set; }

        [Range(0, 5)]
        public int Grade { get; set; }

        public bool IsAlik(Rating entity)
        {
            return this.Commentary == entity.Commentary && this.Grade == entity.Grade &&
                this.ComicBook == entity.ComicBook && this.User == entity.User;
        }
    }
}

using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class ComicBookIllustrator : IAlikeable<ComicBookIllustrator>
    {
        public int ComicBookId { get; set; }
        public virtual ComicBook ComicBook { get; set; }
        public int IllustratorId { get; set; }
        public virtual Illustrator Illustrator { get; set; }

        public bool IsAlik(ComicBookIllustrator entity)
        {
            return this.ComicBook == entity.ComicBook &&
                this.Illustrator == entity.Illustrator;
        }
    }
}

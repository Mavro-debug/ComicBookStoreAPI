using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class Illustrator : IAlikeable<Illustrator>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ComicBookIllustrator> ComicBookIllustrators { get; set; }

        public bool IsAlik(Illustrator entity)
        {
            return this.Name == entity.Name;
        }
    }
}

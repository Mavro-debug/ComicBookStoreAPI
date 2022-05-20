using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class Translator : IAlikeable<Translator>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsAlik(Translator entity)
        {
            return this.Name == entity.Name;
        }
    }
}

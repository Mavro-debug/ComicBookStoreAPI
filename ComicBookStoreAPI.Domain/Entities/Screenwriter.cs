using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class Screenwriter : IAlikeable<Screenwriter>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsAlik(Screenwriter entity)
        {
            return this.Name == entity.Name;
        }
    }
}

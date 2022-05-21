using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class Series : IAlikeable<Series>, IEntityWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsAlik(Series entity)
        {
            return this.Name == entity.Name;
        }
    }
}

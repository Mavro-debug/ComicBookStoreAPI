using ComicBookStoreAPI.Domain.Interfaces.Repositories;


namespace ComicBookStoreAPI.Domain.Entities
{
    public class CoverType : IAlikeable<CoverType>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsAlik(CoverType entity)
        {
            return this.Name == entity.Name;
        }
    }
}

using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class HeroesTeams : IAlikeable<HeroesTeams>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ComicBookHeroesTeams> ComicBookHeroesTeams { get; set; }

        public bool IsAlik(HeroesTeams entity)
        {
            return this.Name == entity.Name;
        }
    }
}

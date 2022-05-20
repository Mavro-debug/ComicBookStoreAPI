using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Domain.Entities
{
    public class ComicBookHeroesTeams : IAlikeable<ComicBookHeroesTeams>
    {
        public int ComicBookId { get; set; }
        public virtual ComicBook ComicBook { get; set; }
        public int HeroesTeamsId { get; set; }
        public virtual HeroesTeams HeroesTeams { get; set; }

        public bool IsAlik(ComicBookHeroesTeams entity)
        {
            return this.ComicBook == entity.ComicBook &&
                this.HeroesTeams == entity.HeroesTeams;
        }
    }
}

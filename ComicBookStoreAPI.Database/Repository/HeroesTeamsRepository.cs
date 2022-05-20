using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Database.Repository
{
    public class HeroesTeamsRepository : IRepository<HeroesTeams>
    {
        private ApplicationDbContext _dbContext;
        public HeroesTeamsRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        private HeroesTeams? IsAnyAlike(HeroesTeams entity)
        {
            var allEntities = _dbContext.HeroesTeams;

            foreach (var item in allEntities)
            {
                var resoult = entity.IsAlik(item);

                if (resoult)
                {
                    return item;
                }
            }

            return null;

        }

            public HeroesTeams Create(HeroesTeams entity)
        {

            _dbContext.HeroesTeams.Add(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (success)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to add new object of type {entity.GetType()}");
            }
        }

        public void Delete(HeroesTeams entity)
        {
            _dbContext.HeroesTeams.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.HeroesTeams.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.HeroesTeams.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(HeroesTeams)} with id {entityId}");
            }

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<HeroesTeams> GetAll()
        {
            return _dbContext.HeroesTeams.ToList();
        }

        public HeroesTeams GetById(int id)
        {
            var entity = _dbContext.HeroesTeams.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(HeroesTeams)} with id {id}");
            }
        }

        public HeroesTeams GetOrCreate(HeroesTeams entity)
        {
            var resoultEntity = this.IsAnyAlike(entity);

            if (resoultEntity == null)
            {
                var translatorCreated = this.Create(entity);

                return translatorCreated;
            }

            return resoultEntity;
        }

        public List<HeroesTeams> GetOrCreateRange(List<HeroesTeams> entities)
        {
            List<HeroesTeams> resoultList = new List<HeroesTeams>();

            foreach (HeroesTeams entity in entities)
            {
                var resoultEntity = this.IsAnyAlike(entity);

                if (resoultEntity == null)
                {
                    var seriesCreated = this.Create(entity);

                    resoultList.Add(seriesCreated);
                }
                else
                {
                    resoultList.Add(resoultEntity);
                }

            }

            return resoultList;
        }
        public void Update(HeroesTeams entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }
    }
}

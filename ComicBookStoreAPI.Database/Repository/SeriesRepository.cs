using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Database.Repository
{
    public class SeriesRepository : IRepository<Series>
    {
        private ApplicationDbContext _dbContext;
        public SeriesRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Series? IsAnyAlike(Series entity)
        {
            var allEntities = _dbContext.Series;

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


        public Series Create(Series entity)
        {

            _dbContext.Series.Add(entity);

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

        public void Delete(Series entity)
        {
            _dbContext.Series.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.Series.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Series)} with id {entityId}");
            }

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<Series> GetAll()
        {
            return _dbContext.Series.ToList();
        }

        public Series GetById(int id)
        {
            var entity = _dbContext.Series.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Series)} with id {id}");
            }
        }

        public Series GetOrCreate(Series entity)
        {
            var resoultEntity = this.IsAnyAlike(entity);

            if (resoultEntity == null)
            {
                var translatorCreated = this.Create(entity);

                return translatorCreated;
            }

            return resoultEntity;
        }

        public List<Series> GetOrCreateRange(List<Series> entities)
        {
            List<Series> resoultList = new List<Series>();

            foreach (Series entity in entities)
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

        public void Update(Series entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }
    }
}

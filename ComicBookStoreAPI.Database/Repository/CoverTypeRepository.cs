using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Database.Repository
{
    public class CoverTypeRepository : IRepository<CoverType>
    {
        private ApplicationDbContext _dbContext;
        public CoverTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private CoverType? IsAnyAlike(CoverType entity)
        {
            var allEntities = _dbContext.CoverTypes;

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


        public CoverType Create(CoverType entity)
        {

            _dbContext.CoverTypes.Add(entity);

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

        public void Delete(CoverType entity)
        {
            _dbContext.CoverTypes.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.CoverTypes.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.CoverTypes.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(CoverType)} with id {entityId}");
            }

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<CoverType> GetAll()
        {
            return _dbContext.CoverTypes.ToList();
        }

        public CoverType GetById(int id)
        {
            var entity = _dbContext.CoverTypes.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(CoverType)} with id {id}");
            }
        }

        public CoverType GetOrCreate(CoverType entity)
        {
            var resoultEntity = this.IsAnyAlike(entity);

            if (resoultEntity == null)
            {
                var translatorCreated = this.Create(entity);

                return translatorCreated;
            }

            return resoultEntity;
        }

        public List<CoverType> GetOrCreateRange(List<CoverType> entities)
        {
            List<CoverType> resoultList = new List<CoverType>();

            foreach (CoverType entity in entities)
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

        public void Update(CoverType entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }   
    }
}

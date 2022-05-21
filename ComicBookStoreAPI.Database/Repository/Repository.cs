using ComicBookStoreAPI.Database.Helpers;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntityWithId, IAlikeable<T>
    {
        private ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Create(T entity)
        {

            _dbContext.Add(entity);

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

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            DbSet<T> allEntities = _dbContext.Set<T>();

            var entity = allEntities.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(T)} with id {entityId}");
            }

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            DbSet<T> allEntities = _dbContext.Set<T>();

            var entity = allEntities.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(T)} with id {id}");
            }
        }

        public T GetOrCreate(T entity)
        {
            var resoultEntity = EntityHelper.IsAnyAlike(entity, _dbContext);

            if (resoultEntity == null)
            {
                var translatorCreated = this.Create(entity);

                return translatorCreated;
            }

            return resoultEntity;
        }

        public List<T> GetOrCreateRange(List<T> entities)
        {
            List<T> resoultList = new List<T>();

            foreach (T entity in entities)
            {
                var resoultEntity = EntityHelper.IsAnyAlike(entity, _dbContext);

                if (resoultEntity == null)
                {
                    var translatorCreated = this.Create(entity);

                    resoultList.Add(translatorCreated);
                }
                else
                {
                    resoultList.Add(resoultEntity);
                }

            }

            return resoultList;
        }

        public void Update(T entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }
    }
}

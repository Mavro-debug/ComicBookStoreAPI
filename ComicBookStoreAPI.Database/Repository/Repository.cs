using ComicBookStoreAPI.Database.Helpers;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Helpers;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntityWithId, IAlikeable<T>
    {
        private ApplicationDbContext _dbContext;
        private readonly IEntityHelper _entityHelper;
        public Repository(ApplicationDbContext dbContext, IEntityHelper entityHelper)
        {
            _dbContext = dbContext;
            _entityHelper = entityHelper;
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
            var resoultEntity = _entityHelper.GetFirstOrDefaultAlike(entity);

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
                var resoultEntity = _entityHelper.GetFirstOrDefaultAlike(entity);

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

        public void Update(int id, T entity)
        {
            DbSet<T> allEntities = _dbContext.Set<T>();

            var entityToUpdate = allEntities.FirstOrDefault(x => x.Id == id);

            if (entityToUpdate == null)
            {
                throw new DatabaseException($"Entity of type {entity.GetType()} and Id: {id} could not be found");
            }

            var entiType = entity.GetType();

            var entityTypePropsInfo = entiType.GetProperties();

            foreach (var propInfo in entityTypePropsInfo)
            {
                if (propInfo.GetValue(entity) != null && propInfo.Name != "Id")
                {
                    propInfo.SetValue(entityToUpdate, propInfo.GetValue(entity));
                }
            }

            _dbContext.SaveChanges();
        }
    }
}

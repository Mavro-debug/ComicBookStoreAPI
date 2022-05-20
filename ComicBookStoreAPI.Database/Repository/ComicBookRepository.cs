using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;


namespace ComicBookStoreAPI.Database.Repository
{
    public class ComicBookRepository : IRepository<ComicBook>
    {
        private ApplicationDbContext _dbContext;
        public ComicBookRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ComicBook? IsAnyAlike(ComicBook entity)
        {
            var allEntities = _dbContext.ComicBooks;

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


        public ComicBook Create(ComicBook entity)
        {

            _dbContext.ComicBooks.Add(entity);

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

        public void Delete(ComicBook entity)
        {
            _dbContext.ComicBooks.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.ComicBooks.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.ComicBooks.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(ComicBook)} with id {entityId}");
            }

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<ComicBook> GetAll()
        {
            return _dbContext.ComicBooks.ToList();
        }

        public ComicBook GetById(int id)
        {
            var entity = _dbContext.ComicBooks.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(ComicBook)} with id {id}");
            }
        }

        public ComicBook GetOrCreate(ComicBook entity)
        {
            var resoultEntity = this.IsAnyAlike(entity);

            if (resoultEntity == null)
            {
                var translatorCreated = this.Create(entity);

                return translatorCreated;
            }

            return resoultEntity;
        }

        public List<ComicBook> GetOrCreateRange(List<ComicBook> entities)
        {
            List<ComicBook> resoultList = new List<ComicBook>();

            foreach (ComicBook entity in entities)
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

        public void Update(ComicBook entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }
    }
}

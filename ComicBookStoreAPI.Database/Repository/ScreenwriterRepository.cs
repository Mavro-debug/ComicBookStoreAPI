using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;


namespace ComicBookStoreAPI.Database.Repository
{
    public class ScreenwriterRepository : IRepository<Screenwriter>
    {
        private ApplicationDbContext _dbContext;
        public ScreenwriterRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Screenwriter? IsAnyAlike(Screenwriter entity)
        {
            var allEntities = _dbContext.Screenwriters;

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



        public Screenwriter Create(Screenwriter entity)
        {

            _dbContext.Screenwriters.Add(entity);

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

        public void Delete(Screenwriter entity)
        {
            _dbContext.Screenwriters.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.Screenwriters.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.Screenwriters.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Screenwriter)} with id {entityId}");
            }

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<Screenwriter> GetAll()
        {
            return _dbContext.Screenwriters.ToList();
        }

        public Screenwriter GetById(int id)
        {
            var entity = _dbContext.Screenwriters.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Screenwriter)} with id {id}");
            }
        }

        public Screenwriter GetOrCreate(Screenwriter entity)
        {
            var entityFound = this.IsAnyAlike(entity);

            if (entityFound != null)
            {
                return entityFound;
            }

            var entityCreated = this.Create(entity);

            return entityCreated;
;
        }

        public List<Screenwriter> GetOrCreateRange(List<Screenwriter> entities)
        {
            List<Screenwriter> resoultList = new List<Screenwriter>();

            foreach (Screenwriter entity in entities)
            {
                var entityFound = this.IsAnyAlike(entity);

                if (entityFound == null)
                {
                    var seriesCreated = this.Create(entity);

                    resoultList.Add(seriesCreated);
                }
                else
                {
                    resoultList.Add(entityFound);
                }

            }

            return resoultList;
        }

        public void Update(Screenwriter entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }
    }
}


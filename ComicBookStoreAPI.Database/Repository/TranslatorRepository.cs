using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;

namespace ComicBookStoreAPI.Database.Repository
{
    public class TranslatorRepository : IRepository<Translator>
    {
        private ApplicationDbContext _dbContext;
        public TranslatorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Translator? IsAnyAlike(Translator entity)
        {
            var allEntities = _dbContext.Translators;

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


        public Translator Create(Translator entity)
        {

            _dbContext.Translators.Add(entity);

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



        public void Delete(Translator entity)
        {
            _dbContext.Translators.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.Translators.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Translator)} with id {entityId}");
            }
           
            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<Translator> GetAll()
        {
            return _dbContext.Translators.ToList();
        }

        public Translator GetById(int id)
        {
            var entity = _dbContext.Translators.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Translator)} with id {id}");
            }
        }

        public Translator GetOrCreate(Translator entity)
        {
            var resoultEntity = this.IsAnyAlike(entity);

            if (resoultEntity == null)
            {
                var translatorCreated = this.Create(entity);

                return translatorCreated;
            }

            return resoultEntity;
        }

        public List<Translator> GetOrCreateRange(List<Translator> entities)
        {
            List<Translator> resoultList = new List<Translator>();

            foreach (Translator entity in entities)
            {
                var resoultEntity = this.IsAnyAlike(entity);

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

        public void Update(Translator entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }
    }
    
}

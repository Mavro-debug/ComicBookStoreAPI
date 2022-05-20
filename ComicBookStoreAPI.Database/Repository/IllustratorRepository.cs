using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Database.Repository
{
    public class IllustratorRepository : IRepository<Illustrator>
    {
        private ApplicationDbContext _dbContext;
        public IllustratorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Illustrator? IsAnyAlike(Illustrator entity)
        {
            var allEntities = _dbContext.Illustrators;

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


        public Illustrator Create(Illustrator entity)
        {

            _dbContext.Illustrators.Add(entity);

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

        public void Delete(Illustrator entity)
        {
            _dbContext.Illustrators.Remove(entity);

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }

        }

        public void Delete(int entityId)
        {
            var entity = _dbContext.Illustrators.Where(t => t.Id == entityId).FirstOrDefault();

            if (entity != null)
            {
                _dbContext.Illustrators.Remove(entity);
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Illustrator)} with id {entityId}");
            }

            bool success = _dbContext.SaveChanges() > 0;

            if (!success)
            {
                throw new DatabaseException($"Data base was not able to remove new object of type {entity.GetType()}");
            }
        }

        public List<Illustrator> GetAll()
        {
            return _dbContext.Illustrators.ToList();
        }

        public Illustrator GetById(int id)
        {
            var entity = _dbContext.Illustrators.Where(t => t.Id == id).FirstOrDefault();

            if (entity != null)
            {
                return entity;
            }
            else
            {
                throw new DatabaseException($"Data base was not able to find new object of type {typeof(Illustrator)} with id {id}");
            }
        }

        public Illustrator GetOrCreate(Illustrator entity)
        {
            var resoultEntity = this.IsAnyAlike(entity);

            if (resoultEntity == null)
            {
                var translatorCreated = this.Create(entity);

                return translatorCreated;
            }

            return resoultEntity;
        }

        public List<Illustrator> GetOrCreateRange(List<Illustrator> entities)
        {
            List<Illustrator> resoultList = new List<Illustrator>();

            foreach (Illustrator entity in entities)
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

        public void Update(Illustrator entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} with Id: {entity.Id} without making changes.");
            }
        }
    }
}

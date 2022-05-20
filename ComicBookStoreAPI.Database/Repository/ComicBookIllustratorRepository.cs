using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Database.Repository
{
    public class ComicBookIllustratorRepository : IRepository<ComicBookIllustrator, ComicBook, Illustrator>
    {
        private ApplicationDbContext _dbContext;
        public ComicBookIllustratorRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private ComicBookIllustrator? IsAnyAlike(ComicBookIllustrator entity)
        {
            var allEntities = _dbContext.ComicBooksIllustrators
                .Include(e => e.Illustrator)
                .Include(e => e.ComicBook);

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


        public void AddToList(List<ComicBookIllustrator> entitiesList, ComicBook entity, bool entityExistsCheck = true)
        {
            if (!entityExistsCheck)
            {
                entitiesList.Add(new ComicBookIllustrator() { ComicBook = entity });
            }
            else
            {
                var resoult = entitiesList.Any(ci => ci.ComicBook.IsAlik(entity));

                if (!resoult)
                {
                    entitiesList.Add(new ComicBookIllustrator() { ComicBook = entity });
                }
            }

        }

        public void AddToList(List<ComicBookIllustrator> entitiesList, Illustrator entity, bool entityExistsCheck = true)
        {
            if (!entityExistsCheck)
            {
                entitiesList.Add(new ComicBookIllustrator() { Illustrator = entity });
            }
            else
            {
                var resoult = entitiesList.Any(ci => ci.Illustrator.IsAlik(entity));

                if (!resoult)
                {
                    entitiesList.Add(new ComicBookIllustrator() { Illustrator = entity });
                }
            }

        }

        public List<ComicBookIllustrator> AssignRange(ComicBook entity, IEnumerable<Illustrator> entitiesList, bool checkIfAssigned = true)
        {
            List<ComicBookIllustrator> createdEntitiesList = new List<ComicBookIllustrator>();

            foreach (var item in entitiesList)
            {
                ComicBookIllustrator entityToBeAdded = new ComicBookIllustrator() { ComicBook = entity, Illustrator = item };

                if (checkIfAssigned)
                {
                    var assigned = this.IsAnyAlike(entityToBeAdded);

                    if (assigned == null)
                    {
                        createdEntitiesList.Add(entityToBeAdded);
                    }
                }
                else
                {
                    createdEntitiesList.Add(entityToBeAdded);
                }
                
            }

            _dbContext.ComicBooksIllustrators.AddRange(createdEntitiesList);

            return createdEntitiesList;
        }

        public List<ComicBookIllustrator> AssignRange(Illustrator entity, IEnumerable<ComicBook> entitiesList, bool checkIfAssigned = true)
        {
            List<ComicBookIllustrator> createdEntitiesList = new List<ComicBookIllustrator>();

            foreach (var item in entitiesList)
            {
                ComicBookIllustrator entityToBeAdded = new ComicBookIllustrator() { Illustrator = entity, ComicBook = item };

                if (checkIfAssigned)
                {
                    var assigned = this.IsAnyAlike(entityToBeAdded);

                    if (assigned == null)
                    {
                        createdEntitiesList.Add(entityToBeAdded);
                    }
                }
                else
                {
                    createdEntitiesList.Add(entityToBeAdded);
                }

            }

            _dbContext.ComicBooksIllustrators.AddRange(createdEntitiesList);

            return createdEntitiesList;
        }


        public List<ComicBookIllustrator> CreateRange(IEnumerable<ComicBook> entitiesList)
        {
            List<ComicBookIllustrator> createdEntitiesList = new List<ComicBookIllustrator>()
;
            foreach (ComicBook entity in entitiesList)
            {
                createdEntitiesList.Add(new ComicBookIllustrator() { ComicBook = entity});
            }

            return createdEntitiesList;
        }

        public List<ComicBookIllustrator> CreateRange(IEnumerable<Illustrator> entitiesList)
        {
            List<ComicBookIllustrator> createdEntitiesList = new List<ComicBookIllustrator>()
;
            foreach (Illustrator entity in entitiesList)
            {
                createdEntitiesList.Add(new ComicBookIllustrator() { Illustrator = entity });
            }

            return createdEntitiesList;
        }



        public int DeleteOutsideRange(List<Illustrator> rangeOfentities, ComicBook entity)
        {

            var entityList = new List<ComicBookIllustrator>();
            var entities = _dbContext.ComicBooksIllustrators
                .Include(e => e.Illustrator)
                .Include(e => e.ComicBook); ;

            foreach (var listEntity in entities)
            {
                bool alike = listEntity.ComicBook.IsAlik(entity);

                if (alike)
                {
                    entityList.Add(listEntity);
                }
            }

            List<ComicBookIllustrator> toBeDeleted = new List<ComicBookIllustrator>();

            foreach (var entitesFromList in entityList)
            {
                foreach (var entitesFormRange in rangeOfentities)
                {
                    if (!entitesFromList.Illustrator.IsAlik(entitesFormRange))
                    {
                        toBeDeleted.Add(entitesFromList);
                    }
                }
            }

            _dbContext.ComicBooksIllustrators.RemoveRange(toBeDeleted);

            return _dbContext.SaveChanges();
        }

        public int DeleteOutsideRange(List<ComicBook> rangeOfentities, Illustrator entity)
        {

            var entityList = new List<ComicBookIllustrator>();

            var entities = _dbContext.ComicBooksIllustrators
                .Include(e => e.Illustrator)
                .Include(e => e.ComicBook); ;

            foreach (var listEntity in entities)
            {
                bool alike = listEntity.Illustrator.IsAlik(entity);

                if (alike)
                {
                    entityList.Add(listEntity);
                }
            }

            List<ComicBookIllustrator> toBeDeleted = new List<ComicBookIllustrator>();

            foreach (var entitesFormList in entityList)
            {
                foreach (var entitesFormRange in rangeOfentities)
                {
                    if (!entitesFormList.ComicBook.IsAlik(entitesFormRange))
                    {
                        toBeDeleted.Add(entitesFormList);
                    }
                }
            }

            _dbContext.ComicBooksIllustrators.RemoveRange(toBeDeleted);

            return _dbContext.SaveChanges();
        }

        public ComicBookIllustrator GetOrCreate(ComicBook firstEntity, Illustrator secondEntity)
        {
            ComicBookIllustrator newComicBookIllustrator = new ComicBookIllustrator() { ComicBook = firstEntity, Illustrator = secondEntity };

            var entityExists = this.IsAnyAlike(newComicBookIllustrator);

            if (entityExists != null)
            {
                return entityExists;
            }

            _dbContext.ComicBooksIllustrators.Add(newComicBookIllustrator);

            bool resoult = _dbContext.SaveChanges() != 0;

            if (!resoult)
            {
                throw new DatabaseException($"Data base was not able to add new object of type {typeof(ComicBookIllustrator)}");
            }

            return newComicBookIllustrator;
        }

        public ComicBookIllustrator GetOrCreate(ComicBookIllustrator entity)
        {

            var entityExists = this.IsAnyAlike(entity);

            if (entityExists != null)
            {
                return entityExists;
            }

            _dbContext.ComicBooksIllustrators.Add(entity);

            bool resoult = _dbContext.SaveChanges() != 0;

            if (!resoult)
            {
                throw new DatabaseException($"Data base was not able to add new object of type {typeof(ComicBookIllustrator)}");
            }

            return entity;
        }


        public void Update(ComicBookIllustrator entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} without making changes.");
            }
        }
    }
}

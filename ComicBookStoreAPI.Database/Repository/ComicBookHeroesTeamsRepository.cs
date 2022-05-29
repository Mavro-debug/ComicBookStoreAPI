using ComicBookStoreAPI.Database.Helpers;
using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Exceptions;
using ComicBookStoreAPI.Domain.Interfaces.Helpers;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ComicBookStoreAPI.Database.Repository
{
    public class ComicBookHeroesTeamsRepository : IRepository<ComicBookHeroesTeams, ComicBook, HeroesTeams>
    {
        private ApplicationDbContext _dbContext;
        private readonly IEntityHelper _entityHelper;

        public ComicBookHeroesTeamsRepository(ApplicationDbContext dbContext, IEntityHelper entityHelper)
        {
            _dbContext = dbContext;
            _entityHelper = entityHelper;
        }




        public void AddToList(List<ComicBookHeroesTeams> entitiesList, ComicBook entity, bool entityExistsCheck = true)
        {
            if (!entityExistsCheck)
            {
                entitiesList.Add(new ComicBookHeroesTeams() { ComicBook = entity });
            }
            else
            {
                var resoult = entitiesList.Any(ci => ci.ComicBook.IsAlik(entity));

                if (!resoult)
                {
                    entitiesList.Add(new ComicBookHeroesTeams() { ComicBook = entity });
                }
            }

        }

        public void AddToList(List<ComicBookHeroesTeams> entitiesList, HeroesTeams entity, bool entityExistsCheck = true)
        {
            if (!entityExistsCheck)
            {
                entitiesList.Add(new ComicBookHeroesTeams() { HeroesTeams = entity });
            }
            else
            {
                var resoult = entitiesList.Any(ci => ci.HeroesTeams.IsAlik(entity));

                if (!resoult)
                {
                    entitiesList.Add(new ComicBookHeroesTeams() { HeroesTeams = entity });
                }
            }

        }

        public List<ComicBookHeroesTeams> AssignRange(ComicBook entity, IEnumerable<HeroesTeams> entitiesList, bool checkIfAssigned = true)
        {
            List<ComicBookHeroesTeams> createdEntitiesList = new List<ComicBookHeroesTeams>();

            foreach (var item in entitiesList)
            {
                ComicBookHeroesTeams entityToBeAdded = new ComicBookHeroesTeams() { ComicBook = entity, HeroesTeams = item };

                if (checkIfAssigned)
                {
                    var assigned = _entityHelper.GetFirstOrDefaultAlike(entityToBeAdded);

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

            _dbContext.ComicBooksHeroesTeams.AddRange(createdEntitiesList);

            return createdEntitiesList;
        }

        public List<ComicBookHeroesTeams> AssignRange(HeroesTeams entity, IEnumerable<ComicBook> entitiesList, bool checkIfAssigned = true)
        {
            List<ComicBookHeroesTeams> createdEntitiesList = new List<ComicBookHeroesTeams>();

            foreach (var item in entitiesList)
            {
                ComicBookHeroesTeams entityToBeAdded = new ComicBookHeroesTeams() { HeroesTeams = entity, ComicBook = item };

                if (checkIfAssigned)
                {
                    var assigned = _entityHelper.GetFirstOrDefaultAlike(entityToBeAdded);

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

            _dbContext.ComicBooksHeroesTeams.AddRange(createdEntitiesList);

            return createdEntitiesList;
        }

        public List<ComicBookHeroesTeams> CreateRange(IEnumerable<ComicBook> entitiesList)
        {
            List<ComicBookHeroesTeams> createdEntitiesList = new List<ComicBookHeroesTeams>()
;
            foreach (ComicBook entity in entitiesList)
            {
                createdEntitiesList.Add(new ComicBookHeroesTeams() { ComicBook = entity });
            }

            return createdEntitiesList;
        }

        public List<ComicBookHeroesTeams> CreateRange(IEnumerable<HeroesTeams> entitiesList)
        {
            List<ComicBookHeroesTeams> createdEntitiesList = new List<ComicBookHeroesTeams>()
;
            foreach (HeroesTeams entity in entitiesList)
            {
                createdEntitiesList.Add(new ComicBookHeroesTeams() { HeroesTeams = entity });
            }

            return createdEntitiesList;
        }



        public int DeleteOutsideRange(List<HeroesTeams> rangeOfentities, ComicBook entity)
        {
            var entityList = new List<ComicBookHeroesTeams>();
            var entities = _dbContext.ComicBooksHeroesTeams
                .Include(e => e.HeroesTeams)
                .Include(e => e.ComicBook); ;

            foreach (var listEntity in entities)
            {
                bool alike = listEntity.ComicBook.IsAlik(entity);

                if (alike)
                {
                    entityList.Add(listEntity);
                }
            }

            List<ComicBookHeroesTeams> toBeDeleted = new List<ComicBookHeroesTeams>();

            foreach (var entitesFormList in entityList)
            {
                foreach (var entitesFormRange in rangeOfentities)
                {
                    if (!entitesFormList.HeroesTeams.IsAlik(entitesFormRange))
                    {
                        toBeDeleted.Add(entitesFormList);
                    }
                }
            }

            _dbContext.ComicBooksHeroesTeams.RemoveRange(toBeDeleted);

            return _dbContext.SaveChanges();
        }

        public int DeleteOutsideRange(List<ComicBook> rangeOfentities, HeroesTeams entity)
        {
            var entityList = new List<ComicBookHeroesTeams>();

            var entities = _dbContext.ComicBooksHeroesTeams
                .Include(e => e.HeroesTeams)
                .Include(e => e.ComicBook); ;

            foreach (var listEntity in entities)
            {
                bool alike = listEntity.HeroesTeams.IsAlik(entity);

                if (alike)
                {
                    entityList.Add(listEntity);
                }
            }

            List<ComicBookHeroesTeams> toBeDeleted = new List<ComicBookHeroesTeams>();

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

            _dbContext.ComicBooksHeroesTeams.RemoveRange(toBeDeleted);

            return _dbContext.SaveChanges();
        }

        public ComicBookHeroesTeams GetOrCreate(ComicBook firstEntity, HeroesTeams secondEntity)
        {
            ComicBookHeroesTeams newComicBookHerosTeams = new ComicBookHeroesTeams() { ComicBook = firstEntity, HeroesTeams = secondEntity };

            var entityExists = _entityHelper.GetFirstOrDefaultAlike(newComicBookHerosTeams);

            if (entityExists != null)
            {
                return entityExists;
            }

            _dbContext.ComicBooksHeroesTeams.Add(newComicBookHerosTeams);

            bool resoult = _dbContext.SaveChanges() != 0;

            if (!resoult)
            {
                throw new DatabaseException($"Data base was not able to add new object of type {typeof(ComicBookIllustrator)}");
            }

            return newComicBookHerosTeams;
        }

        public ComicBookHeroesTeams GetOrCreate(ComicBookHeroesTeams entity)
        {

            var entityExists = _entityHelper.GetFirstOrDefaultAlike(entity);

            if (entityExists != null)
            {
                return entityExists;
            }

            _dbContext.ComicBooksHeroesTeams.Add(entity);

            bool resoult = _dbContext.SaveChanges() != 0;

            if (!resoult)
            {
                throw new DatabaseException($"Data base was not able to add new object of type {typeof(ComicBookHeroesTeams)}");
            }

            return entity;
        }



        public void Update(ComicBookHeroesTeams entity)
        {
            var noChanges = _dbContext.SaveChanges() == 0;

            if (noChanges)
            {
                throw new DatabaseException($"An attempt to update entity type: {entity.GetType()} without making changes.");
            }
        }
    }
}

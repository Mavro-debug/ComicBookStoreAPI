using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Database.Repository
{
    public class Repository<T, F, S> : IRepository<T, F, S>
        where T : class, IAlikeable<T>, new()
        where F : class, IEntityWithId, IAlikeable<F>
        where S : class, IEntityWithId, IAlikeable<S>
    {
        private ApplicationDbContext _dbContext;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddToList(List<T> entitiesList, F entity, bool entityExistsCheck = true)
        {
            if (!entityExistsCheck)
            {
                var typeT = typeof(T);
                var propertiesInfoTypeT = typeT.GetProperties();

                var entityToBeAdded = new T();

                foreach (var propInfoT in propertiesInfoTypeT)
                {
                    var valueT = propInfoT.GetValue(entityToBeAdded);
                    if (valueT != null)
                    {
                        if (valueT.GetType() == entity.GetType())
                        {
                            propInfoT.SetValue(entityToBeAdded, entity);
                        }
                    }

                }

                _dbContext.Add(entityToBeAdded);

            }
            else
            {
                //var resoult = entitiesList.Any(ci => ci.ComicBook.IsAlik(entity));
            }


/*            if (!entityExistsCheck)
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
            }*/

        }

        public void AddToList(List<T> entitiesList, S entity, bool entityExistsCheck = true)
        {
            throw new NotImplementedException();
        }

        public List<T> AssignRange(F entity, IEnumerable<S> entitiesList, bool checkIfAssigned = true)
        {
            throw new NotImplementedException();
        }

        public List<T> AssignRange(S entity, IEnumerable<F> entitiesList, bool checkIfAssigned = true)
        {
            throw new NotImplementedException();
        }

        public List<T> CreateRange(IEnumerable<F> entitiesList)
        {
            throw new NotImplementedException();
        }

        public List<T> CreateRange(IEnumerable<S> entitiesList)
        {
            throw new NotImplementedException();
        }

        public int DeleteOutsideRange(List<S> rangeOfentities, F entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteOutsideRange(List<F> rangeOfentities, S entity)
        {
            throw new NotImplementedException();
        }

        public T GetOrCreate(F firstEntity, S secondEntity)
        {
            throw new NotImplementedException();
        }

        public T GetOrCreate(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

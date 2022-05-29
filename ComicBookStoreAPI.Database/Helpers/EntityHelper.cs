using ComicBookStoreAPI.Domain.Entities;
using ComicBookStoreAPI.Domain.Interfaces.Helpers;
using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;


namespace ComicBookStoreAPI.Database.Helpers
{

    public class EntityHelper : IEntityHelper
    {
        private readonly ApplicationDbContext _dbContext;

        public EntityHelper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public bool IsAnyAlike<T>(T entity) where T : class, IAlikeable<T>
        {
            DbSet<T> allEntities = _dbContext.Set<T>();

            foreach (var item in allEntities)
            {
                var resoult = entity.IsAlik(item);

                if (resoult)
                {
                    return true;
                }
            }

            return false;
        }

        public T? GetFirstOrDefaultAlike<T>(T entity) where T : class, IAlikeable<T>
        {
            DbSet<T> allEntities = _dbContext.Set<T>();

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


    }
}

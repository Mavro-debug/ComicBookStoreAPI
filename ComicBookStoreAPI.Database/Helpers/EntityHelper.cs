using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Database.Helpers
{
    public static class EntityHelper
    {
        public static T? IsAnyAlike<T>(T entity, ApplicationDbContext dbConetxt) where T : class, IAlikeable<T>
        {
            DbSet<T> allEntities = dbConetxt.Set<T>();

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

using ComicBookStoreAPI.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Interfaces.Helpers
{
    public interface IEntityHelper
    {
        bool IsAnyAlike<T>(T entity) where T : class, IAlikeable<T>;
        T? GetFirstOrDefaultAlike<T>(T entity) where T : class, IAlikeable<T>;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Interfaces.Repositories
{
    public interface IEntityWithId
    {
        public int Id { get; set; }
    }
}

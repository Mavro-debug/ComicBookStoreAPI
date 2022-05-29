using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Interfaces.Repositories
{
    public interface IRepository<T, F, S> where T : IAlikeable<T>
    {
        List<T> AssignRange(F entity, IEnumerable<S> entitiesList, bool checkIfAssigned = true);
        List<T> AssignRange(S entity, IEnumerable<F> entitiesList, bool checkIfAssigned = true);
        int DeleteOutsideRange(List<S> rangeOfentities, F entity);
        int DeleteOutsideRange(List<F> rangeOfentities, S entity);
    }
}

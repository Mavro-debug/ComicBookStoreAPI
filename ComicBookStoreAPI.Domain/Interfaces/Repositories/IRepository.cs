using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookStoreAPI.Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : IAlikeable<T>
    {
        List<T> GetAll();
        T GetById(int id);
        T GetOrCreate(T entity);
        List<T> GetOrCreateRange(List<T> entities);
        T Create(T entity);
        void Delete(T entity);
        void Delete(int entityId);
        void Update(int id, T entity);
    }

    public interface IRepository<T, F, S> where T : IAlikeable<T>
    {
        void AddToList(List<T> entitiesList, F entity, bool entityExistsCheck = true);
        void AddToList(List<T> entitiesList, S entity, bool entityExistsCheck = true);

        List<T> CreateRange(IEnumerable<F> entitiesList);
        List<T> CreateRange(IEnumerable<S> entitiesList);   
        List<T> AssignRange(F entity, IEnumerable<S> entitiesList, bool checkIfAssigned = true);
        List<T> AssignRange(S entity, IEnumerable<F> entitiesList, bool checkIfAssigned = true);
        T GetOrCreate(F firstEntity, S secondEntity);
        T GetOrCreate(T entity);
        int DeleteOutsideRange(List<S> rangeOfentities, F entity);
        int DeleteOutsideRange(List<F> rangeOfentities, S entity);
        void Update(T entity);
    }
}

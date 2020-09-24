using System;

namespace DataAccessInterface
{
    public interface IRepository<T> where T:class
    {
        bool Add(T entity);
        bool Delete(T entity);
        bool Exists(T entity);
    }
}

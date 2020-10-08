using Domain;
using System;
using System.Collections.Generic;

namespace DataAccessInterface
{
    public interface IRepository<T> where T : class
    {
        T Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        T Get(int id);
        IEnumerable<T> GetAll(Func<object, bool> filter=null);
    }
}

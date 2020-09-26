using System;
using System.Collections.Generic;

namespace DataAccessInterface
{
    public interface IRepository<T,K> where T:class
    {
        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
        T Get(K key);
        IEnumerable<T> GetAll(Func<object, bool> filter=null);
    }
}

using System;
using System.Collections.Generic;

namespace DataAccessInterface
{
    public interface IRepository<T> where T:class
    {
        bool Add(T entity);
        bool Delete(T entity);
        bool Exists(T entity);
        List<T> Filter(Func<object, bool> p);
        T GetById(int id);
        bool Modify(int id, T entity);
    }
}

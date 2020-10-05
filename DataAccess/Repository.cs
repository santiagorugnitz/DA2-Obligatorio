using DataAccessInterface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private DbContext context;
        private readonly DbSet<T> DbSet;


        public Repository(DbContext context)
        {
            this.DbSet = context.Set<T>();
            this.context = context;
        }

        public bool Add(T entity)
        {
            DbSet.Add(entity);
            Save();
            return true;
        }

        public bool Delete(T entity)
        {
            DbSet.Remove(entity);
            Save();
            return true;
        }

        private void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll(Func<object, bool> p = null)
        {
            return DbSet.ToList().Where(x=>p.Invoke(x));
        }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public bool Update(T entity)
        {
            DbSet.Update(entity);
            Save();
            return true;
        }
    }
}

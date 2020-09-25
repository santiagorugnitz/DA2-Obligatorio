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
        private TourismContext context;
        private readonly DbSet<T> DbSet;


        public Repository(TourismContext context)
        {
            this.DbSet = context.Set<T>();
            this.context = context;
        }

        public bool Add(T entity)
        {
            DbSet.Add(entity);
            return true;
        }

        public bool Delete(T entity)
        {
            DbSet.Remove(entity);
            return true;
        }

        public bool Exists(T entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }
    }
}

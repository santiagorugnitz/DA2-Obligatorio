using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
    {
        private DbContext context;
        private readonly DbSet<Administrator> DbSet;

        public AdministratorRepository(DbContext context) : base(context)
        {
            this.DbSet = context.Set<Administrator>();
            this.context = context;
        }

        public Administrator Find(string email, string password)
        {
            var admins = DbSet.ToList().Where(admin => admin.Email == email
            && admin.Password == password);

            if (admins.Count() == 0)
            {
                return null;
            }

            return admins.ToArray()[0];
        }

        public bool IsLogged(string token)
        {
            if (token == null)
            {
                return false;
            }
            
            var admins = DbSet.ToList().Where(admin => admin.Token == token);

            if (admins.Count() == 0)
            {
                return false;
            }

            return true;
        }
    }
}

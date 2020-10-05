using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(DbContext context) : base(context)
        {
        }

        public Administrator Find(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool IsLogged(string token)
        {
            throw new NotImplementedException();
        }
    }
}

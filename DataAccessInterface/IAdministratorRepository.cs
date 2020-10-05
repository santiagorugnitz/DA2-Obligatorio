using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace DataAccessInterface
{
    public interface IAdministratorRepository : IRepository<Administrator>
    {
        public bool IsLogged(string token);
        public Administrator Find(string email, string password);


    }
}

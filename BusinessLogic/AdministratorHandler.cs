using DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace BusinessLogic
{
    public class AdministratorHandler
    {
        private IRepository<Administrator> repository;

        public AdministratorHandler(IRepository<Administrator> repo)
        {
            repository = repo;
        }

        public bool Add(Administrator administrator)
        {
            return repository.Add(administrator);
        }

        public object Delete(Administrator administrator)
        {
            return repository.Delete(administrator);
        }
    }
}

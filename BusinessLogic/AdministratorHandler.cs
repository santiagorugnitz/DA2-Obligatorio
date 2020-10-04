using DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using BusinessLogicInterface;
using System.Linq;

namespace BusinessLogic
{
    public class AdministratorHandler : IAdministratorHandler
    {
        private IRepository<Administrator> repository;

        public AdministratorHandler(IRepository<Administrator> repo)
        {
            repository = repo;
        }

        public bool Add(Administrator administrator)
        {
            if (repository.GetAll((x => ((Administrator)x).Email == administrator.Email))
                .Count() != 0)
            {
                throw new InvalidOperationException("The mail already exists");
            }

            return repository.Add(administrator);
        }

        public object Delete(Administrator administrator)
        {
            return repository.Delete(administrator);
        }
    }
}

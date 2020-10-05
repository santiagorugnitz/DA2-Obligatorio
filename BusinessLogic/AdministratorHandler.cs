using DataAccessInterface;
using System;
using System.Collections.Generic;
using System.Text;
using Domain;
using BusinessLogicInterface;

namespace BusinessLogic
{
    public class AdministratorHandler : IAdministratorHandler
    {
        private IAdministratorRepository repository;

        public AdministratorHandler(IAdministratorRepository repo)
        {
            repository = repo;
        }

        public bool Add(Administrator administrator)
        {
            return repository.Add(administrator);
        }

        public bool Delete(Administrator administrator)
        {
            return repository.Delete(administrator);
        }

        public string Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public void Logout(string token)
        {
            throw new NotImplementedException();
        }
    }
}

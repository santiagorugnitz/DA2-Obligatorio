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
        private IAdministratorRepository repository;

        public AdministratorHandler(IAdministratorRepository repo)
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

        public bool Delete(int id)
        {
            return repository.Delete(Get(id));
        }

        public Administrator Get(int id)
        {
            return repository.Get(id);
        }

        public List<Administrator> GetAll()
        {
            return repository.GetAll().ToList();
        }

        public string Login(string email, string password)
        {
            var admin = repository.Find(email, password);
            if (admin == null) throw new ArgumentException("Wrong email or password");
            admin.Token = Guid.NewGuid().ToString();
            repository.Update(admin);
            return admin.Token;
        }

        public void Logout(string token)
        {
            var admin = repository.Find(token);
            if (admin == null) return;
            admin.Token = null;
            repository.Update(admin);
        }

        public bool IsLogged(string token)
        {
            return repository.Find(token)!=null;
            
        }

        public bool Update(Administrator administrator)
        {
            return repository.Update(administrator);
        }
    }
}

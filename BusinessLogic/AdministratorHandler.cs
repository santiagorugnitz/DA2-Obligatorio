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

        public Administrator Add(Administrator administrator)
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
            var admin = Get(id);
            if (admin == null) throw new NullReferenceException("There is no admin with that id");
            return repository.Delete(admin);
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
            var admin = Get(administrator.Id);
            if (admin == null) throw new NullReferenceException("There is no admin with that id");
            admin.Name = administrator.Name;
            admin.Email = administrator.Email;
            admin.Password = administrator.Password;
            return repository.Update(admin);
        }
    }
}

using Domain;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface IAdministratorHandler
    {
        Administrator Add(Administrator administrator);
        Administrator Get(int id);
        List<Administrator> GetAll();
        bool Update(Administrator administrator);
        bool Delete(int id);
        string Login(string email, string password);
        void Logout(string token);
        bool IsLogged(string token);


    }
}
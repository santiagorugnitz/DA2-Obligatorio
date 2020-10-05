using Domain;

namespace BusinessLogicInterface
{
    public interface IAdministratorHandler
    {
        bool Add(Administrator administrator);
        bool Delete(Administrator administrator);
        string Login(string email, string password);
        void Logout(string token);


    }
}
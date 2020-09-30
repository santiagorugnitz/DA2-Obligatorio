using Domain;

namespace BusinessLogicInterface
{
    public interface IAdministratorHandler
    {
        bool Add(Administrator administrator);
        object Delete(Administrator administrator);
    }
}
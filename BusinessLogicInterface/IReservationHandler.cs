using Domain;

namespace BusinessLogicInterface
{
    public interface IReservationHandler
    {
        bool Add(Reservation reservation);
        object ChangeState(Reservation reservation, ReservationState state, string description);
        Reservation CheckState(int id);
        object Delete(Reservation reservation);
    }
}
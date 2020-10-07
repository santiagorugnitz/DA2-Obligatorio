using Domain;

namespace BusinessLogicInterface
{
    public interface IReservationHandler
    {
        bool Add(Reservation reservation, int accomodationId);
        bool ChangeState(Reservation reservation, ReservationState state, string description);
        Reservation CheckState(int id);
        bool Delete(Reservation reservation);
    }
}
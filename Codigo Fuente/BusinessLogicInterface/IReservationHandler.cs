using Domain;
using System.Collections.Generic;

namespace BusinessLogicInterface
{
    public interface IReservationHandler
    {
        Reservation Add(Reservation reservation, int accommodationId);
        bool ChangeState(int idReservation, ReservationState state, string description);
        Reservation CheckState(int id);
        bool Delete(Reservation reservation);
        List<Reservation> GetAllFromaccommodation(int spotId);
        bool Review(int id, double score, string comment);
    }
}
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;

namespace BusinessLogic
{
    public class ReservationHandler : IReservationHandler
    {

        private IRepository<Reservation> repository;
        private IAccomodationHandler accomodationHandler;

        public ReservationHandler(IRepository<Reservation> repo, IAccomodationHandler accomodationHand)
        {
            repository = repo;
            accomodationHandler = accomodationHand;
        }

        public Reservation Add(Reservation reservation, int accomodationId)
        {
            var gotAccomodation = accomodationHandler.Get(accomodationId);
            if (gotAccomodation != null)
            {
                reservation.Accomodation = gotAccomodation;
                return  repository.Add(reservation);
            }
            else
            {
                throw new NullReferenceException("The accomodation does not exists");
            }
        }

        public bool Delete(Reservation reservation)
        {
            return repository.Delete(reservation);
        }

        public Reservation CheckState(int id)
        {
            return repository.Get(id);
        }

        public bool ChangeState(int idReservation, ReservationState state, string description)
        {
            Reservation reservation = repository.Get(idReservation);
            reservation.ReservationState = state;
            reservation.ReservationDescription = description;
            return repository.Update(reservation);
        }
    }
}

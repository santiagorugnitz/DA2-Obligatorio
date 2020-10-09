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
                reservation.Total = CalculateTotal(reservation);
                return  repository.Add(reservation);
            }
            else
            {
                throw new ArgumentOutOfRangeException("There is no accomodation with that id");
            }
        }

        private double CalculateTotal(Reservation reservation)
        {
            double ret = 0;
            int days = (reservation.CheckOut - reservation.CheckIn).Days;
            ret += reservation.AdultQuantity + reservation.ChildrenQuantity*0.5 + reservation.BabyQuantity*0.25;
            ret *= days * reservation.Accomodation.Fee;
            return ret;
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

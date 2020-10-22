using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

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
                return repository.Add(reservation);
            }
            else
            {
                throw new BadRequestException("There is no accomodation with that id");
            }
        }

        private double CalculateTotal(Reservation reservation)
        {
            double ret = 0;
            int days = (reservation.CheckOut - reservation.CheckIn).Days;
            ret += reservation.AdultQuantity + reservation.ChildrenQuantity * 0.5 + reservation.BabyQuantity * 0.25;
            ret += ((int)reservation.RetiredQuantity / 2) * 0.7;
            ret += reservation.RetiredQuantity % 2;
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
            if (reservation == null)
            {
                throw new NotFoundException("The reservation does not exist");
            }
            reservation.ReservationState = state;
            reservation.StateDescription = description;
            return repository.Update(reservation);
        }

        public List<Reservation> GetAllFromAccomodation(int id)
        {
            if (accomodationHandler.Get(id) == null)
            {
                throw new BadRequestException("The accomodation does not exist");
            }
            return repository.GetAll(x => ((Reservation)x).Accomodation.Id == id).ToList();
        }
    }
}

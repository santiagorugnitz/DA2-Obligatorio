﻿using DataAccessInterface;
using Domain;
using System;

namespace BusinessLogic
{
    public class ReservationHandler
    {

        private IRepository<Reservation> repository;
        private AccomodationHandler accomodationHandler;

        public ReservationHandler(IRepository<Reservation> repo, AccomodationHandler accomodationHand)
        {
            repository = repo;
            accomodationHandler = accomodationHand;
        }

        public bool Add(Reservation reservation)
        {
            if (accomodationHandler.Exists(reservation.Accomodation))
            {
                return repository.Add(reservation);
            }
            else
            {
                throw new NullReferenceException("The tourist spot does not exists");
            }
        }

        public object Delete(Reservation reservation)
        {
            return repository.Delete(reservation);
        }
    }
}

using DataAccessInterface;
using Domain;
using System;

namespace BusinessLogic
{
    public class ReservationHandler
    {

        private IRepository<Reservation> repository;

        public ReservationHandler(IRepository<Reservation> repo)
        {
            repository = repo;
        }

        public bool Add(Reservation reservation)
        {
            return repository.Add(reservation);
        }

        public object Delete(Reservation reservation)
        {
            return repository.Delete(reservation);
        }
    }
}

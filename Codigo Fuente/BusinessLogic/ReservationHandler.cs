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
        private IAccommodationHandler accommodationHandler;

        public ReservationHandler(IRepository<Reservation> repo, IAccommodationHandler accommodationHand)
        {
            repository = repo;
            accommodationHandler = accommodationHand;
        }

        public Reservation Add(Reservation reservation, int accommodationId)
        {
            var gotAccommodation = accommodationHandler.Get(accommodationId);
            if (gotAccommodation == null || !gotAccommodation.Available)
            {
                throw new BadRequestException("There is no available accommodation with that id");
            
            }
            else
            {
                reservation.Accommodation = gotAccommodation;

                var stay = new Stay
                {
                    Adults = new Tuple<int, int>(reservation.AdultQuantity, reservation.RetiredQuantity),
                    ChildrenQuantity = reservation.ChildrenQuantity,
                    BabyQuantity = reservation.BabyQuantity,
                    CheckIn = reservation.CheckIn,
                    CheckOut = reservation.CheckOut
                };

                reservation.Total = accommodationHandler.CalculateTotal(accommodationId,stay);
                return repository.Add(reservation);
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

        public bool Review(int id, double score, string comment)
        {
            Reservation reservation = repository.Get(id);
            if (reservation == null)
            {
                throw new NotFoundException("The reservation does not exist");
            }
            reservation.Score = score;
            reservation.Comment = comment;
            return repository.Update(reservation);
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

        public List<Reservation> GetAllFromAccommodation(int id)
        {
            if (accommodationHandler.Get(id) == null)
            {
                throw new BadRequestException("The accommodation does not exist");
            }
            return repository.GetAll(x => ((Reservation)x).Accommodation.Id == id).ToList();
        }

  
    }
}

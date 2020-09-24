using System;
using System.Collections.Generic;

namespace Domain
{
    public enum ReservationState
    {
        Active
    }
    public class Reservation
    {
        public int Id { get; set; }

        public Accomodation Accomodation { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int AdultQuantity { get; set; }

        public int ChildrenQuantity { get; set; }

        public int BabyQuantity { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public ReservationState ReservationState { get; set; }

        public string ReservationDescription { get; set; }
    }
}
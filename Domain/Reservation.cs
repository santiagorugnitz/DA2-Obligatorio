using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public enum ReservationState
    {
        Creada,
        Pendiente_Pago,
        Aceptada,
        Rechazada,
        Expirada
    }
    public class Reservation
    {
        public int Id { get; set; }

        public virtual Accomodation Accomodation { get; set; }

        private DateTime checkIn;
        public DateTime CheckIn
        {
            get { return checkIn; }
            set
            {
                if (value < DateTime.Today)
                {
                    throw new ArgumentOutOfRangeException("The Check In Date needs to be after today");
                }
                else
                {
                    checkIn = value;
                }
            }
        }

        private DateTime checkOut;
        public DateTime CheckOut
        {
            get { return checkOut; }
            set
            {
                if (value < CheckIn)
                {
                    throw new ArgumentOutOfRangeException("The Check Out Date needs to be after Check In");
                }
                else
                {
                    checkOut = value;
                }
            }
        }

        public int BabyQuantity { get; set; }

        public int ChildrenQuantity { get; set; }

        private int adultQuantity;
        public int AdultQuantity
        {
            get { return adultQuantity; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The Reservation needs at least one adult guest");
                }
                else
                {
                    adultQuantity = value;
                }
            }
        }

        private string name;
        public string Name
        {
            set
            {
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The reservation needs a non empty Name");
                }
                else
                {
                    name = value.Trim();
                }
            }

            get { return name; }
        }

        private string email;
        public string Email
        {
            set
            {
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The reservation needs a non empty Email");
                }
                else
                {
                    email = value.Trim();
                }
            }

            get { return email; }
        }

        private string surname;
        public string Surname
        {
            set
            {
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The reservation needs a non empty surname");
                }
                else
                {
                    surname = value.Trim();
                }
            }

            get { return surname; }
        }

        public ReservationState ReservationState { get; set; }

        public string ReservationDescription { get; set; }
    }
}
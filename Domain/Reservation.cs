﻿using Exceptions;
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
                    throw new BadRequestException("The Check In Date needs to be after today");
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
                    throw new BadRequestException("The Check Out Date needs to be after Check In");
                }
                else
                {
                    checkOut = value;
                }
            }
        }

        private int babyQuantity;
        public int BabyQuantity
        {
            get { return babyQuantity; }
            set
            {
                if (value < 0)
                {
                    throw new BadRequestException("The baby quantity must be 0 or more");
                }
                else
                {
                    babyQuantity = value;
                }
            }
        }

        private int childrenQuantity;
        public int ChildrenQuantity
        {
            get { return childrenQuantity; }
            set
            {
                if (value < 0)
                {
                    throw new BadRequestException("The children quantity must be 0 or more");
                }
                else
                {
                    childrenQuantity = value;
                }
            }
        }

        private int adultQuantity;
        public int AdultQuantity
        {
            get { return adultQuantity; }
            set
            {
                if (value <= 0)
                {
                    throw new BadRequestException("The Reservation needs at least one adult guest");
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The reservation needs a non empty Name");
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The reservation needs a non empty Email");
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
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The reservation needs a non empty surname");
                }
                else
                {
                    surname = value.Trim();
                }
            }

            get { return surname; }
        }

        private ReservationState reservationState;

        public ReservationState ReservationState
        {
            set
            {
                if (!Enum.IsDefined(typeof(ReservationState), value))
                    throw new BadRequestException("Invalid state");
                else
                {
                    reservationState = value;
                }
            }

            get { return reservationState; }
        }

        public string ReservationDescription { get; set; }

        public string StateDescription { get; set; }

        public double Total { get; set; }
    }
}
using Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public enum ReservationState
    {
        Created,
        Payment_Pendent,
        Accepted,
        Rejected,
        Expired
    }
    public class Reservation
    {
        private static readonly double MAX_SCORE = 5.0;
        private static readonly double MIN_SCORE = 1.0;

        public int Id { get; set; }

        public virtual Accommodation Accommodation { get; set; }

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

        private int retiredQuantity;
        public int RetiredQuantity
        {
            get { return retiredQuantity; }
            private set
            {
                if (value < 0)
                {
                    throw new BadRequestException("The retired guests quantity must be 0 or more");
                }
                else
                {
                    retiredQuantity = value;
                }
            }
        }

        private int adultQuantity;
        public int AdultQuantity
        {
            get { return adultQuantity; }
            private set
            {
                if (value < 0)
                {
                    throw new BadRequestException("The adult quantity must be 0 or more");
                }
                else
                {
                    adultQuantity = value;
                }
            }
        }

        public Tuple<int, int> Adults
        {
            set
            {
                if (value.Item1 == 0 && value.Item2 == 0)
                {
                    throw new BadRequestException("The reservation needs at least one adult guest");
                }
                else
                {
                    AdultQuantity = value.Item1;
                    RetiredQuantity = value.Item2;
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
                    throw new BadRequestException("The reservation needs a non empty name");
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

        public string StateDescription { get; set; }

        public double Total { get; set; }

        private double? score;
        public double? Score
        {
            get { return score; }

            set
            {
                if (value == null || value < MIN_SCORE || value > MAX_SCORE)
                {
                    throw new BadRequestException("Score must be between 1 and 5");
                } 
                else
                {
                    score = value;
                }
            }
        }

        public string Comment { get;set; }
    }
}
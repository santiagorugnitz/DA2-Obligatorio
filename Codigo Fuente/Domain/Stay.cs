using Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Stay
    {

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
    }
}

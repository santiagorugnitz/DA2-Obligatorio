using Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Accommodation
    {
        private static readonly double MAX_STARS = 5.0;
        private static readonly double MIN_STARS = 1.0;

        public int Id { get; set; }

        private string name;
        public string Name
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The accommodation needs a non empty Name");
                }
                else
                {
                    name = value.Trim();
                }
            }

            get { return name; }
        }

        private double stars;
        public double Stars
        {
            get { return stars; }

            set
            {
                if (value > MAX_STARS || value < MIN_STARS)
                {
                    throw new BadRequestException("The accommodation stars needs to be between 1 and 5");
                }
                else
                {
                    stars = value;
                }
            }
        }

        private string address;
        public string Address
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The accommodation needs a non empty address");
                }
                else
                {
                    address = value.Trim();
                }
            }

            get { return address; }
        }

        private List<Image> images;
        public virtual List<Image> Images
        {
            get { return images; }
            set
            {
                if (value == null || value.Count() == 0)
                {
                    throw new BadRequestException("The accommodation needs at least one image");
                }
                else
                {
                    images = value;
                }
            }
        }

        private double fee;
        public double Fee
        {
            get { return fee; }

            set
            {
                if (value <= 0)
                {
                    throw new BadRequestException("The accommodation fee needs to be more than 0");
                }
                else
                {
                    fee = value;
                }
            }
        }

        public string Description { get; set; }

        public bool Available { get; set; }

        public string Telephone { get; set; }

        public string ContactInformation { get; set; }

        public virtual TouristSpot TouristSpot { get; set; }

    }
}

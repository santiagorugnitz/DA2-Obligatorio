using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Accomodation
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            set
            {
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The accomodation needs a non empty Name");
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
                if (value > 5.0 || value < 1.0)
                {
                    throw new ArgumentOutOfRangeException("The Accomodation stars needs to be between 1 and 5");
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
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The accomodation needs a non empty address");
                }
                else
                {
                    address = value.Trim();
                }
            }

            get { return address; }
        }

        public List<string> ImageUrlList { get; set; }

        private double fee;
        public double Fee
        {
            get { return fee; }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("The Accomodation fee needs to be more than 0");
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

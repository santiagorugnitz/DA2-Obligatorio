using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ReservationModel
    {
        public string Name { get; set; }

        public int AdultQuantity { get; set; }

        public int ChildrenQuantity { get; set; }
        
        public int BabyQuantity { get; set; }

        public int RetiredQuantity { get; set; }

        public string Email { get; set; }

        public string Surname { get; set; }

        public ReservationState ReservationState { get; set; }


        public string StateDescription { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public int AccomodationId { get; set; }

        public Reservation ToEntity() => new Reservation()
        {
            Name = this.Name,
            Adults = new Tuple<int,int>(AdultQuantity,RetiredQuantity),
            ChildrenQuantity = this.ChildrenQuantity,
            BabyQuantity = this.BabyQuantity,
            Email = this.Email,
            Surname = this.Surname,
            ReservationState = this.ReservationState,
            StateDescription = this.StateDescription,
            CheckIn = this.CheckIn,
            CheckOut = this.CheckOut
        };
    }
}

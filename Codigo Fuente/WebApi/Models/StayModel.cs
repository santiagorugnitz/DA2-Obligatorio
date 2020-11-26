using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace WebApi.Models
{
    public class StayModel
    {
        public int AdultQuantity { get; set; }

        public int ChildrenQuantity { get; set; }

        public int BabyQuantity { get; set; }

        public int RetiredQuantity { get; set; }

        public DateTime CheckIn { get; set; }

        public DateTime CheckOut { get; set; }

        public Stay ToEntity() => new Stay()
        {
            Adults = new Tuple<int, int>(AdultQuantity, RetiredQuantity),
            ChildrenQuantity = this.ChildrenQuantity,
            BabyQuantity = this.BabyQuantity,
            CheckIn = this.CheckIn,
            CheckOut = this.CheckOut
        };
    }
}

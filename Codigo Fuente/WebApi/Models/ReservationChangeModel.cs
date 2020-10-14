using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ReservationChangeModel
    {
        public ReservationState State { get; set; }
        public string Description { get; set; }

    }
}

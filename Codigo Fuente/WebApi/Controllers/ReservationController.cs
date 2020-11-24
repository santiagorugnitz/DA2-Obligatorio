using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [ApiController]
    [Route("reservations")]
    public class ReservationController : Controller
    {
        private readonly IReservationHandler handler;

        public ReservationController(IReservationHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public IActionResult Post([FromBody] ReservationModel reservation)
        {
            var res = handler.Add(reservation.ToEntity(), reservation.AccommodationId);
            return Ok(res.Id);

        }

        [HttpGet("{id}")]
        public IActionResult CheckState(int id)
        {
            var res = handler.CheckState(id);
            if (null == res) throw new NotFoundException("The reservation does not exist");
            return Ok(res);
        }

        [HttpGet()]
        public IActionResult GetFromaccommodation(int accommodationId)
        {
            return Ok(handler.GetAllFromaccommodation(accommodationId));
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult ChangeState(int id, [FromBody] ReservationChangeModel change)
        {
            handler.ChangeState(id, change.State, change.Description);
            return Ok("Reservation state updated");
        }

        [HttpPut("{id}/review")]
        public IActionResult Review(int id, [FromBody] ReviewModel review)
        {
            handler.Review(id, review.Score, review.Comment);
            return Ok("Review submited");
        }
    }
}

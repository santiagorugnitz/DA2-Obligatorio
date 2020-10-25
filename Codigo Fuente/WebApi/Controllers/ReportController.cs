using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [ApiController]
    [Route("report")]
    public class ReportController : Controller
    {
        private readonly IReportHandler handler;
        public ReportController(IReportHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet("{spotId}, {startingDate}, {finishingDate}")]
        public IActionResult Get(int spotId, DateTime startingDate, DateTime finishingDate)
        {
            return Ok(handler.AccomodationsReport(spotId, startingDate, finishingDate));
        }
    }
}

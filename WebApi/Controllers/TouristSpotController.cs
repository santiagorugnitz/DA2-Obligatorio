﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("spots")]
    [ApiController]
    public class TouristSpotController : ControllerBase
    {

        private readonly ITouristSpotHandler handler;


        public TouristSpotController(ITouristSpotHandler handler)
        {
            this.handler = handler;
        }

        [HttpGet]
        public IActionResult GetAll([FromBody] SearchModel search)
        {
            return Ok(handler.Search(search.CategoryIds,search.RegionId));
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = handler.Get(id);
            if (null == res) return NotFound();
            return Ok(res);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TouristSpotModel spot)
        {
            handler.Add(spot.ToEntity(),spot.RegionId,spot.CategoryIds,spot.Image);
            return Ok("Tourist spot added");

        }
    }
}

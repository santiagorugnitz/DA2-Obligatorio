﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorHandler handler;


        public AdministratorController(IAdministratorHandler handler)
        {
            this.handler = handler;
        }

        [HttpPost]
        public IActionResult Post(AdministratorModel adminModel)
        {
            var res = handler.Add(adminModel.ToEntity());
            return Ok(res);
        }
    }
}
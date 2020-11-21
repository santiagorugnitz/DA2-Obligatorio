using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ServiceFilter(typeof(ExceptionFilter))]
    [ApiController]
    [Route("administrators")]
    public class AdministratorController : ControllerBase
    {
        private readonly IAdministratorHandler handler;


        public AdministratorController(IAdministratorHandler handler)
        {
            this.handler = handler;
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPost]
        public IActionResult Post(AdministratorModel adminModel)
        {
           handler.Add(adminModel.ToEntity());
            return Ok("Administrator added");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            return Ok(handler.Login(loginModel.Email, loginModel.Password));
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("logout")]
        public IActionResult Logout([FromHeader] string token)
        {
            handler.Logout(token);
            return Ok("Logged out successfully");
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = handler.Get(id);
            if (null == res) throw new NotFoundException("The Administrator does not exists");
            return Ok(res);
        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            handler.Delete(id);
            return Ok("Administrator deleted");

        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpPut("{id}")]
        public IActionResult Update(int id, AdministratorModel adminModel)
        {
            var admin = adminModel.ToEntity();
            admin.Id = id;
            handler.Update(admin);
            return Ok("Administrator updated");

        }

        [ServiceFilter(typeof(AuthorizationFilter))]
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(handler.GetAll());
        }

    }
}

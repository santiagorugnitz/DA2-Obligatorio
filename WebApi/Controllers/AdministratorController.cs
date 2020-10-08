using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
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
            handler.Login(loginModel.Email, loginModel.Password);
            return Ok("Logged successfully");

        }

        [HttpDelete("logout")]
        public IActionResult Logout([FromHeader] string token)
        {
            handler.Logout(token);
            return Ok("Logged out successfully");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var res = handler.Get(id);
            if (null == res) return NotFound();
            return Ok(res);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            handler.Delete(id);
            return Ok("Administrator deleted");

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AdministratorModel adminModel)
        {
            var admin = adminModel.ToEntity();
            admin.Id = id;
            handler.Update(admin);
            return Ok("Administrator updated");

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(handler.GetAll());
        }

    }
}

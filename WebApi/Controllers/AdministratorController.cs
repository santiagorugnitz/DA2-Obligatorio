using System;
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
    [Route("administrators")]
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            return Ok(handler.Login(loginModel.Email, loginModel.Password));
        }

        [HttpDelete("logout")]
        public IActionResult Logout([FromHeader] string token)
        {
            handler.Logout(token);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(handler.Get(id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(handler.Delete(id));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, AdministratorModel adminModel)
        {
            var admin = adminModel.ToEntity();
            admin.Id = id;
            return Ok(handler.Update(admin));
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(handler.GetAll());
        }
    }
}

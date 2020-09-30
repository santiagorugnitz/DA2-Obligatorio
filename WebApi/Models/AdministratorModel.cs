using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class AdministratorModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        public Administrator ToEntity() => new Administrator()
        {
            Name = this.Name,
            Email = this.Email,
            Password = this.Password
        };
    }
}

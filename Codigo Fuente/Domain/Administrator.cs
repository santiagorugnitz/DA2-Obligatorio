using Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Administrator
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The administrator needs a non empty Name");
                }
                else
                {
                    name = value.Trim();
                }
            }

            get { return name; }
        }

        private string email;
        public string Email
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The administrator needs a non empty Email");
                }
                else
                {
                    email = value.Trim();
                }
            }

            get { return email; }
        }

        private string password;
        public string Password
        {
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The administrator needs a non empty Password");
                }
                else
                {
                    password = value.Trim();
                }
            }

            get { return password; }
        }
        public string Token { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Administrator admin &&
                   Id == admin.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email, Password);
        }
    }
}

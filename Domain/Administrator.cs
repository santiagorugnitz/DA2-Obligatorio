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
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The administrator needs a non empty Name");
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
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The administrator needs a non empty Email");
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
                if (value.Trim() == "")
                {
                    throw new ArgumentNullException("The administrator needs a non empty Password");
                }
                else
                {
                    password = value.Trim();
                }
            }

            get { return password; }
        }
    }
}

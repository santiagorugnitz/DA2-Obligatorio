using Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Image
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new BadRequestException("The image needs to be non empty");
                }
                else
                {
                    name = value.Trim();
                }
            }
        }
    }
}

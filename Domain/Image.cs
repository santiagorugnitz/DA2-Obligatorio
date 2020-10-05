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
                if (value.Trim().Length == 0)
                {
                    throw new ArgumentNullException("The spot needs a non empty picture URL");
                }
                else
                {
                    name = value.Trim();
                }
            }
        }
    }
}

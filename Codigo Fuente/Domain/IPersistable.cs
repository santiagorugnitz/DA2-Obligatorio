using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IPersistable
    {
        int Id { get; set; }
    }
}

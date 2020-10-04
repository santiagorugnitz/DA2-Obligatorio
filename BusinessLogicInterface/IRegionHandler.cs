using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IRegionHandler
    {
        List<Region> GetAll();
    }
}

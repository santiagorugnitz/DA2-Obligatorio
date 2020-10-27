using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IFormatterHandler
    {
        List<string> GetAll();
        bool Add(int position,string fileName);
    }
}

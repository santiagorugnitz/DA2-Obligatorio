using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DataImport;

namespace BusinessLogicInterface
{
    public interface IFormatterHandler
    {
        List<string> GetAll();
        bool Add(int position, List<SourceParameter> parameters);
    }
}

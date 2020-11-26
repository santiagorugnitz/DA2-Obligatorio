using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using DataImport;

namespace BusinessLogicInterface
{
    public interface IDllHandler
    {
        IEnumerable<IImporter> GetDlls();
    }
}

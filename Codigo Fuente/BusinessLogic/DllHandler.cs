using BusinessLogicInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DataImport;

namespace BusinessLogic
{
    public class DllHandler : IDllHandler
    {
        public string ImportersPath { get; set; }

        public DllHandler()
        {
            this.ImportersPath = Directory.GetCurrentDirectory() + "\\Formatters";
        }
        public IEnumerable<IFormatter> GetDlls()
        {
            throw new NotImplementedException();
        }
    }
}

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
            this.ImportersPath = Directory.GetCurrentDirectory() + "\\Importers";
        }
        public IEnumerable<IImporter> GetDlls()
        {
            string[] files = Directory.GetFiles(ImportersPath, "*.dll");
            foreach (string file in files)
            {
                Assembly dll = Assembly.UnsafeLoadFrom(file);
                Type type = dll.GetTypes().Where(i => typeof(IImporter).IsAssignableFrom(i)).FirstOrDefault();
                if (type != null)
                    yield return Activator.CreateInstance(type) as IImporter;
            }
        }
    }
}

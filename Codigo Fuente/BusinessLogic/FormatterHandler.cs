using BusinessLogicInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BusinessLogic
{
    public class FormatterHandler : IFormatterHandler
    {
        private IDllHandler handler;

        public FormatterHandler(IDllHandler hand)
        {
            handler = hand;
        }

        public bool Add(int position, string fileName)
        {
            var dlls = handler.GetDlls();
            return dlls.ElementAt(position).UploadFromFile(fileName);
        }

        public List<string> GetAll()
        {
            var dlls = handler.GetDlls();
            List<string> dllNames = new List<string>();

            foreach (var dll in dlls)
            {
                dllNames.Add(dll.GetName());
            }

            return dllNames;
        }
    }
}

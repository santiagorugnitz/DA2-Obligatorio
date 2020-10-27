using BusinessLogicInterface;
using Domain;
using Exceptions;
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
            if (position > dlls.Count()-1 || position < 0) throw new BadRequestException("The dll position does not exists");

            var dll = dlls.ElementAt(position);
            if (!fileName.EndsWith("." + dll.GetName())) throw new BadRequestException("The file does not have the correct extension");

            var wasUploaded = dll.UploadFromFile(fileName);
            if (!wasUploaded) throw new BadRequestException("The file does not exists");

            return wasUploaded;
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

        public List<string> GetFileNames(int id)
        {
            throw new NotImplementedException();
        }
    }
}

using BusinessLogicInterface;
using Domain;
using Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DataImport;


namespace BusinessLogic
{
    public class FormatterHandler : IFormatterHandler
    {
        private IDllHandler handler;
        private IAccomodationHandler accomodationHandler;

        public FormatterHandler(IDllHandler hand, IAccomodationHandler accomodationHand)
        {
            handler = hand;
            accomodationHandler = accomodationHand;
        }

        public bool Add(int position, List<SourceParameter> parameters)
        {
            var dlls = handler.GetDlls();
            if (position > dlls.Count()-1 || position < 0) throw new BadRequestException("The dll position does not exists");

            var dll = dlls.ElementAt(position);

            var accomodations = dll.Upload(parameters);
            var wasSuccesfull = accomodationHandler.Add(accomodations);
            return wasSuccesfull;
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

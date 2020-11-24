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
    public class ImporterHandler : IImporterHandler
    {
        private IDllHandler handler;
        private IaccommodationHandler accommodationHandler;

        public ImporterHandler(IDllHandler hand, IaccommodationHandler accommodationHand)
        {
            handler = hand;
            accommodationHandler = accommodationHand;
        }

        public bool Add(int position, List<SourceParameter> parameters)
        {
            var dlls = handler.GetDlls();
            if (position > dlls.Count()-1 || position < 0) throw new BadRequestException("The dll position does not exists");

            var dll = dlls.ElementAt(position);

            var accommodations = dll.Upload(parameters);
            var wasSuccesfull = accommodationHandler.Add(accommodations);
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
    }
}

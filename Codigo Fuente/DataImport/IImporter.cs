using Domain;
using System;
using System.Collections.Generic;

namespace DataImport
{
    public interface IImporter
    {
        string GetName();
        List<AccomodationImport> Upload(List<SourceParameter> sourceParameters);
    }

    public class SourceParameter
    {
        public ParameterType Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public enum ParameterType
    {
        File,
        String,
        Date,
        Int
    }
}

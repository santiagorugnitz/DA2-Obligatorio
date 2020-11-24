using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using DataImport;


namespace JsonImporter
{
    public class JsonImporter : IImporter
    {
        public string GetName()
        {
            return "json";
        }

        public List<accommodationImport> Upload(List<SourceParameter> sourceParameters)
        {
            var param = sourceParameters[0];

            return JsonConvert.DeserializeObject<List<accommodationImport>>(File.ReadAllText(param.Value));
        }
    }
}

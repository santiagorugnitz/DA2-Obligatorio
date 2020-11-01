using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using DataImport;


namespace JsonFormatter
{
    public class JsonFormatter : IFormatter
    {
        public string GetName()
        {
            return "json";
        }

        public List<AccomodationImport> Upload(List<SourceParameter> sourceParameters)
        {
            var param = sourceParameters[0];

            return JsonConvert.DeserializeObject<List<AccomodationImport>>(File.ReadAllText(param.Value));
        }
    }
}

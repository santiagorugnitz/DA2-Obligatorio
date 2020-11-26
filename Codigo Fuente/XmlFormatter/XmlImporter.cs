using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using DataImport;
using Microsoft.VisualBasic.CompilerServices;

namespace XmlImporter
{
    public class XmlImporter: IImporter
    {
        public string GetName()
        {
            return "xml";
        }

        public List<AccommodationImport> Upload(List<SourceParameter> sourceParameters)
        {
            var param = sourceParameters[0];

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<AccommodationImport>), new XmlRootAttribute("AccommodationImports"));

            FileStream fs = new FileStream(param.Value, FileMode.Open);
            
            var ret = (List < AccommodationImport > )xmlSerializer.Deserialize(fs);

            fs.Close();
            return ret;

        }

    }
}

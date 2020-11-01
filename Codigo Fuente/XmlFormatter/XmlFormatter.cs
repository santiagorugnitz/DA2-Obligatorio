using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DataImport;
namespace XmlFormatter
{
    public class XmlFormatter: IFormatter
    {
        public string GetName()
        {
            return "xml";
        }

        public List<AccomodationImport> Upload(List<SourceParameter> sourceParameters)
        {
            var param = sourceParameters[0];



            //XDocument doc = XDocument.Load(param.Value);

            //foreach (XElement el in doc.Root.Elements())
            //{
            //    Console.WriteLine("{0} {1}", el.Name, el.Attribute("id").Value);
            //    Console.WriteLine("  Attributes:");
            //    foreach (XAttribute attr in el.Attributes())
            //        Console.WriteLine("    {0}", attr);
            //    Console.WriteLine("  Elements:");

            //    foreach (XElement element in el.Elements())
            //        Console.WriteLine("    {0}: {1}", element.Name, element.Value);
            //}


            return new List<AccomodationImport>();

        }
    }
}

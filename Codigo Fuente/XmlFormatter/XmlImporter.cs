using System;
using System.Collections.Generic;
using System.Xml.Linq;
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

        public List<accommodationImport> Upload(List<SourceParameter> sourceParameters)
        {
            var param = sourceParameters[0];

            var ret = new List<accommodationImport>();

            XDocument doc = XDocument.Load(param.Value);

            foreach (XElement el in doc.Root.Elements())
            {
                ret.Add(new accommodationImport
                {
                    Name = el.Attribute("name").Value,
                    Stars = Double.Parse(el.Attribute("stars").Value),
                    Address = el.Attribute("address").Value,
                    ImageNames = GetImages(el),
                    Fee = Double.Parse(el.Attribute("fee").Value),
                    Description = el.Attribute("description").Value,
                    Available = Boolean.Parse(el.Attribute("available").Value),
                    Telephone = el.Attribute("telephone").Value,
                    ContactInformation = el.Attribute("contact_information").Value,
                    TouristSpot = GetSpot(el.Element("TouristSpotImport"))
                }) ;
            }
            return ret;

        }

        private TouristSpotImport GetSpot(XElement spot)
        {
            return new TouristSpotImport
            {
                Name = spot.Attribute("name").Value,
                Description = spot.Attribute("description").Value,
                RegionId = int.Parse(spot.Attribute("region_id").Value),
                Image = spot.Attribute("image").Value,
                CategoryIds = GetCategories(spot),
            };
        }

        private List<int> GetCategories(XElement spot)
        {
            var ret = new List<int>();

            foreach (var cat in spot.Element("Categories").Elements())
            {
                ret.Add(int.Parse(cat.Attribute("id").Value));
            }
            return ret;
        }

        private List<string> GetImages(XElement accommodation)
        {
            var ret = new List<string>();

            foreach (var image in accommodation.Element("Images").Elements())
            {
                ret.Add(image.Attribute("name").Value);
            }
            return ret;

        }
    }
}

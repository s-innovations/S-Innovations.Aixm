using Newtonsoft.Json.Linq;
using SInnovations.Aixm.Extensions;
using SInnovations.Aixm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Terradue.GeoJson.Geometry;

namespace SInnovations.Aixm.Converters
{


    class AIXMAirspaceGeometryComponentConverter
    {
        [Export]
        [AixmConverterMetadata(AIXMElementName = "AirspaceGeometryComponent", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
           
            XNamespace aximNs = "http://www.aixm.aero/schema/5.1";
            var AirspaceVolume = element.Descendants(aximNs + "AirspaceVolume").ToArray();
            if (AirspaceVolume.Length != 1)
                throw new NotSupportedException("AirSpaceVolme must be of length 1");


            return converter.ReadElement(currentObject, AirspaceVolume.Elements());


        }

        private static XmlElement DownCastToPointElement(XElement element)
        {
            XNamespace ns = "http://www.opengis.net/gml/3.2";
            XmlDocument doc = new XmlDocument();
          
            element.Name = ns+ "Point";         
            
            return doc.ReadNode(element.CreateReader()) as XmlElement;
           
        }
    }
}

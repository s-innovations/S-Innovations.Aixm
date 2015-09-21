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


    class AIXMElevatedPointConverter
    {
        [Export]
        [AixmConverterMetadata(AIXMElementName = "ElevatedPoint", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            XNamespace aximNs = "http://www.aixm.aero/schema/5.1";

            //Convert ElevatedPoint to a GML Surface and convert to geojson.
            var geometry = GeometryFactory.GmlToGeometry(DownCastToPointElement(element));
                       
            //Set the geometry
            currentObject.SetGeometry(geometry);

            //Set the projection on the feature obj.
            currentObject.SetSrs(element);

            //Read elevation property and return
            return converter.ReadElement(currentObject, element.Elements(aximNs + "elevation"));
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

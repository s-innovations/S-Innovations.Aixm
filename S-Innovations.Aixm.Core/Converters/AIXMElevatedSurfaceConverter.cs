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

    /// <summary>
    /// Converts between ElevatedSurfaces and GeoJSON, where ElevatedSurfaces seems to be extensions ontop of GML Surfaces with a elevation added.
    /// 
    /// The elevation is set on the current features properties as a elevation property.
    /// </summary>
    class AIXMElevatedSurfaceConverter
    {
        XNamespace aximNs = "http://www.aixm.aero/schema/5.1";

        [Export]
        [AixmConverterMetadata(AIXMElementName = "Surface", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadSurfaceElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
           
            //Convert ElevatedSurface to a GML Surface and convert to geojson.
            var geometry = GeometryFactory.GmlToGeometry(DownCastToSurfaceElement(element));

            //Set the geometry
            currentObject.SetGeometry(geometry);

            //Set the projection on the feature obj.
            currentObject.SetSrs(element);

            return Enumerable.Empty<JObject>();

          
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "ElevatedSurface", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElevatedSurfaceElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
          
            //Read elevation property and return
            return ReadSurfaceElement(converter, currentObject, element).Concat(
                converter.ReadElement(currentObject, element.Elements(aximNs + "elevation")));
        }

        private static XmlElement DownCastToSurfaceElement(XElement element)
        {
            XNamespace ns = "http://www.opengis.net/gml/3.2";
            XmlDocument doc = new XmlDocument();
          
            element.Name = ns+ "Surface";         
            
            return doc.ReadNode(element.CreateReader()) as XmlElement;
           
        }
    }
}

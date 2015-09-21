using Newtonsoft.Json.Linq;
using SInnovations.Aixm.Extensions;
using SInnovations.Aixm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SInnovations.Aixm.Converters
{
    class AIXMGeometriesConverter
    {
        /// <summary>
        /// Converts from AIXM Extent Property to a Geojson Geometry of the current object.
        /// 
        /// Extents seems to be represented by ElevatedSurface <seealso cref="AIXMElevatedSurfaceConverter"/>
        /// </summary>
        [Export]
        [AixmConverterMetadata(AIXMElementName = "extent", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadExtentElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
         
            currentObject.AddToProperties("geometrySource", "extent");
            //set the ElementType of the extent on feature property extentSource, to be used when writing back.
            currentObject.AddToProperties("extentSource", element.Elements().First().Name.LocalName);

            return converter.ReadElement(currentObject, element.Elements());           
        }


        [Export]
        [AixmConverterMetadata(AIXMElementName = "part", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadPartElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            
            currentObject.AddToProperties("geometrySource", "part");
            //set the ElementType of the part on feature property partSource, to be used when writing back.
            currentObject.AddToProperties("partSource", element.Elements().First().Name.LocalName);


            return converter.ReadElement(currentObject, element.Elements());        
        }

        /// <summary>
        /// Found elements are sub types of the parent. 
        /// </summary>        
        [Export]
        [AixmConverterMetadata(AIXMElementName = "element", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElementElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
           
            var elementElement = element.Elements().ToArray();
            if(elementElement.Length != 1)
            {
                throw new NotSupportedException("ReadElementElement do not support more than one nested element");
            }
         
            var id = currentObject["properties"]["id"];
            currentObject.SetGeometry(new JObject());
            var obj = new JObject(new JProperty("properties",new JObject(
                new JProperty("elementSource",elementElement[0].Name.LocalName),
                new JProperty("parent", id is JObject ? id["#text"].ToString() : id.ToString())
                )));

            //Need to yield the current feature when going to sub types.
            //When no sub features are returned the current feature is automaticly returned for all other cases.
            yield return currentObject;

            foreach (var feature in converter.ReadElement(obj, elementElement[0].Elements()).ToList())
            {
                yield return feature;
            }
            
       
          
        }
        /// <summary>
        /// Location elements is points and sofar seen as subelements of elements above.
        /// </summary>
     
        [Export]
        [AixmConverterMetadata(AIXMElementName = "location", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadLocationElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            return converter.ReadElement(currentObject, element.Elements());
           
        }

        [Export]
        [AixmConverterMetadata(AIXMElementName = "geometryComponent", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadGeometryComponenentElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("geometrySource", element.Elements().First().Name.LocalName);

            return converter.ReadElement(currentObject, element.Elements());
          //  currentObject.Add("geometry", JObject.FromObject(element));
          

            return Enumerable.Empty<JObject>();
        }
    }
}

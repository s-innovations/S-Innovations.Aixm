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

namespace SInnovations.Aixm.Converters
{
    public class GMLIdentifierConvertor
    {

        [Export]
        [AixmConverterMetadata(AIXMElementName = "identifier", AIXMVersion="*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("id", JObject.FromObject(element));

            //If not a feature return null;
            return Enumerable.Empty<JObject>();
        }

        [Export]
        [AixmConverterMetadata(WriteOrder = 1)]
        public void WriteElement(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            var id = feature["properties"]["id"];

            if (flow == 0)
            {
                if (id != null)
                {
                    if (id is JObject)
                    {
                        writer.WriteStartElement("gml", "identifier", null);
                        var obj = id as JObject;
                        foreach(var prop in obj.Properties().Where(p=>p.Name.StartsWith("@")))
                        { 
                            writer.WriteAttributeString(prop.Name.Substring(1), prop.Value.ToString());
                        }
                        writer.WriteValue(id["#text"].ToString());
                        writer.WriteEndElement();


                    }else{
                        writer.WriteElementString("gml", "identifier", null, id.ToString());
                    }
                }   
            }
            else
            {
               

            }
        }
    }
}

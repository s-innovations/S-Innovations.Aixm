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
    class MessageHasMemberConverter
    {
        [Export]
        [AixmConverterMetadata(AIXMElementName = "hasMember", AIXMElementScheme="message", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            var AixmElement = element.Elements().First();
            var feature = new JObject(new JProperty("type","Feature"));
            feature.AddToProperties("AixmType", AixmElement.Name.LocalName.ToString());
            return converter.ReadElement(feature, AixmElement.Elements());
        }

        [Export]
        [AixmConverterMetadata(WriteOrder = 0)]
        public void WriteElementStart(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            var type = feature["properties"]["AixmType"];
            if (flow == 0)
            {
                writer.WriteStartElement("message", "hasMember", null);
                if (type != null)
                {
                    writer.WriteStartElement("aixm", type.ToString(),null);
                }
               
            }
            else
            {
                if (type != null)
                {
                    writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }


        }
    }
}

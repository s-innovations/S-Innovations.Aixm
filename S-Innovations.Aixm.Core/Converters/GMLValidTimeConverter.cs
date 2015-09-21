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
    public class GMLValidTimeConverter
    {
        //TODO write and read the times properly, proof of concept.
        [Export]
        [AixmConverterMetadata(AIXMElementName = "validTime", AIXMVersion="*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("avalibility", JObject.FromObject(element));

            //If not a feature return null;
            return Enumerable.Empty<JObject>();
        }

        [Export]
        [AixmConverterMetadata(WriteOrder = 10)]
        public void WriteElement(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            var validTime = feature["properties"]["avalibility"];
              var type = feature["properties"]["AixmType"];
              if (type != null)
              {
                  if (flow == 0)
                  {
                      writer.WriteStartElement("aixm", "timeSlice", null);
                      writer.WriteStartElement("aixm", type.ToString() + "TimeSlice", null);
                      if (validTime != null)
                      {
                          writer.WriteStartElement("gml", "validTime", null);
                          writer.WriteStartElement("gml", "TimePeriod", null);

                          writer.WriteElementString("gml", "beginPosition", null, "");
                          writer.WriteElementString("gml", "endPosition", null, "");

                          writer.WriteEndElement();
                          writer.WriteEndElement();
                      }
                     
                  }
                  else
                  {
                      writer.WriteEndElement();
                      writer.WriteEndElement();
                  }
              }
        }
    }
}

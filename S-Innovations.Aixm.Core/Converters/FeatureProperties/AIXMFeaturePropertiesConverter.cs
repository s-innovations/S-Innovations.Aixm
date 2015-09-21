using Newtonsoft.Json;
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
    
    public class AIXMFeaturePropertiesConverter
    {
        private static void WriteStringElement(JObject feature, XmlWriter writer, string key, string prefix, string name)
        {
            if (feature["properties"][key] != null)
            {
                writer.WriteElementString(prefix, name, null, feature["properties"][key].ToString());
            }
        }

        //Could make the attribute take a list of acceptable elements names.
        [Export]
        [AixmConverterMetadata(AIXMElementName = "interpretation", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadInterpretationElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("interpretation", element.Value);

            return Enumerable.Empty<JObject>();
        }

        [Export]
        [AixmConverterMetadata(WriteOrder = 100)]
        public void WriteInterpretationElement(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            if (flow == 0)
            {
                WriteStringElement(feature, writer, "interpretation", "aixm", "interpretation");
            }
        }

       

        [Export]
        [AixmConverterMetadata(AIXMElementName = "sequenceNumber", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadSequenceNumberElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            int v;
            currentObject.AddToProperties("sequenceNumber", int.TryParse(element.Value, out v) ? (JToken)v:(JToken)element.Value);

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(WriteOrder = 100)]
        public void WriteSequenceNumberElement(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            if (flow == 0)
            {
                WriteStringElement(feature, writer, "sequenceNumber", "aixm", "sequenceNumber");
            }
        }

        [Export]
        [AixmConverterMetadata(AIXMElementName = "type", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadTypeElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("aixmType", element.Value);

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(WriteOrder = 100)]
        public void WriteAixmTypeElement(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            if (flow == 0)
            {
                WriteStringElement(feature, writer, "aixmType", "aixm", "type");
            }
        }


        [Export]
        [AixmConverterMetadata(AIXMElementName = "surfaceProperties", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadSurfacePropertiesElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("surfaceProperties", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }

        //Todo, create writer



        [Export]
        [AixmConverterMetadata(AIXMElementName = "name", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadNameElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("name", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(WriteOrder = 100)]
        public void WriteNameElement(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            if (flow == 0)
            {
                WriteStringElement(feature, writer, "name", "aixm", "name");
            }
        }


        [Export]
        [AixmConverterMetadata(AIXMElementName = "associatedApron", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadAssociatedApronElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
    
            currentObject.AddToProperties("associatedApron", JToken.FromObject(element.Attributes().ToDictionary(a => a.Name, a => a.Value)));

            return Enumerable.Empty<JObject>();
        }

        [Export]
        [AixmConverterMetadata(AIXMElementName = "correctionNumber", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadCorrectionNumberElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("correctionNumber", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(WriteOrder = 100)]
        public void WriteCorrectionNumberElement(IAixmConverter converter, int flow, JObject feature, XmlWriter writer)
        {
            if (flow == 0)
            {
                WriteStringElement(feature, writer, "correctionNumber", "aixm", "correctionNumber");
            }
        }


        [Export]
        [AixmConverterMetadata(AIXMElementName = "timeSliceMetadata", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadTimeSliceMetadataElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("timeSliceMetadata", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "colour", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadColourElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("colour", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "availability", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadAvailabilityElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("availability", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "verticalExtent", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadVerticalExtentElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("verticalExtent", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }

        [Export]
        [AixmConverterMetadata(AIXMElementName = "upperLimit", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadUpperLimitElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("upperLimit", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }

        [Export]
        [AixmConverterMetadata(AIXMElementName = "upperLimitReference", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadUpperLimitReferenceElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("upperLimitReference", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "lowerLimitReference", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadLowerLimitReferenceElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("lowerLimitReference", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "lowerLimit", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadLowerLimitElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("lowerLimit", JObject.FromObject(element));

            return Enumerable.Empty<JObject>();
        }
        
    }
}

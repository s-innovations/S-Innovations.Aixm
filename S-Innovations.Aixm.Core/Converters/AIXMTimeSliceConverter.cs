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
    class AIXMTimeSliceConverter
    {
        [Export]
        [AixmConverterMetadata(AIXMElementName = "timeSlice", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            var AixmTimeSlice = element.Elements().First();

          
            currentObject.AddToProperties("AixmTimeSlice", JToken.FromObject(AixmTimeSlice.Attributes().ToDictionary(a=>a.Name,a=>a.Value)));

            return converter.ReadElement(currentObject, AixmTimeSlice.Elements());
        }
    }
}

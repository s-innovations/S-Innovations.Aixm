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
    class AIXMElevationConvtertor
    {
        [Export]
        [AixmConverterMetadata(AIXMElementName = "elevation", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("elevation", JObject.FromObject(element));

            //If not a feature return null;
            return Enumerable.Empty<JObject>();
        }
    }
}

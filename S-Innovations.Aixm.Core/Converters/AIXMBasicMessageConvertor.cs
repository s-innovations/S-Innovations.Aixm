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
using System.Xml.Linq;

namespace SInnovations.Aixm.Converters
{
    public class AIXMBasicMessageConvertor
    {
        [Export]
        [AixmConverterMetadata(AIXMElementName = "AIXMBasicMessage", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            currentObject.AddToProperties("#type",element.Name.ToString());

            currentObject.Add("type", "FeatureCollection");
            currentObject.Add("features", converter.ReadElements(currentObject,element.Elements()));

           yield return currentObject;
        }
    }
}

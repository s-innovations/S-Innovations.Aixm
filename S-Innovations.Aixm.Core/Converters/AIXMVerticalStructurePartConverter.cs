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
    class AIXMVerticalStructurePartConverter
    {
        [Export]
        [AixmConverterMetadata(AIXMElementName = "VerticalStructurePart", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadElement(IAixmConverter converter, JObject currentObject, XElement element)
        {
            
            return converter.ReadElement(currentObject, element.Elements());
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "horizontalProjection_surfaceExtent", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadSurfaceExtentElement(IAixmConverter converter, JObject currentObject, XElement element)
        {

            return converter.ReadElement(currentObject, element.Elements());
        }
        [Export]
        [AixmConverterMetadata(AIXMElementName = "horizontalProjection", AIXMVersion = "*")]
        public IEnumerable<JObject> ReadhorizontalProjectionElement(IAixmConverter converter, JObject currentObject, XElement element)
        {

            return converter.ReadElement(currentObject, element.Elements());
        }
    }
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SInnovations.Aixm.Services
{
    public interface IAixmConverter
    {

        IEnumerable<JObject> ReadElement(JObject currentObject, XElement element);
        IEnumerable<JObject> ReadElement(JObject currentObject, IEnumerable<System.Xml.Linq.XElement> enumerable);
        JArray ReadElements(JObject currentArray,IEnumerable<System.Xml.Linq.XElement> enumerable);
    }
}

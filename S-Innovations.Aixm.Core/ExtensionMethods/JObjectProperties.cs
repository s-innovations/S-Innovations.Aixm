using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SInnovations.Aixm
{
    public static class JObjectProperties
    {
        public static JObject AddToProperties(this JObject currentObject, string key, JToken value)
        {
            JToken properties;
            if (!currentObject.TryGetValue("properties", out properties))
            {
                properties = new JObject();
                currentObject.Add("properties", properties);
            }
            (properties as JObject)[key] = Simplify(value);
            return properties as JObject;
        }

        private static JToken Simplify(JToken value)
        {
            if(value is JObject)
            {
                var obj = value as JObject;
                var props = obj.Properties().ToArray();
                if(props.Length == 1)
                {
                    return props[0].Value;
                }
            }
            return value;
        }
        public static JObject AddToProperties(this JObject currentObject, object content)
        {
            JToken properties;
            if (!currentObject.TryGetValue("properties", out properties))
            {
                properties = new JObject();
                currentObject.Add("properties", properties);
            }
            (properties as JObject).Add(content);
            return properties as JObject;
        }
        public static JArray AddToGeometryCollection(this JObject currentObject, object content)
        {
            JToken properties;
            if (!currentObject.TryGetValue("geometry", out properties))
            {
                properties = new JObject(new JProperty("type", "GeometryCollection"), new JProperty("geometries", new JArray()));
                currentObject.Add("geometry", properties);
            }
            var geometries = (properties as JObject)["geometries"] as JArray;
            geometries.Add(content);
            return geometries;
        }
        public static void SetGeometry(this JObject currentObject, object geometry)
        {
            currentObject["geometry"] = JObject.FromObject(geometry);
        }

        public static void SetSrs(this JObject currentObject, XElement element)
        {
            if (element.Attribute("srsName") != null)
            {
                currentObject.Add("crs", new JObject(new JProperty("type", "name"),
                new JProperty("properties", new JObject(new JProperty("name", element.Attribute("srsName").Value)))));
            }
        }
    }
}

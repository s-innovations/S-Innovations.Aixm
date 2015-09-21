using Newtonsoft.Json.Linq;
using SInnovations.Aixm.Converters;
using SInnovations.Aixm.Extensions;
using SInnovations.Aixm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace SInnovations.Aixm
{
    public class AixmConverter : IAixmConverter
    {
        private List<Lazy<Func<IAixmConverter, JObject, XElement, IEnumerable<JObject>>, IAixmConverterMetadata>> _elementReaders = new List<Lazy<Func<IAixmConverter, JObject, XElement, IEnumerable<JObject>>, IAixmConverterMetadata>>();
        private List<Lazy<Action<IAixmConverter,int, JObject, XmlWriter>, IAixmConverterMetadata>> _elementWriters =
            new List<Lazy<Action<IAixmConverter,int, JObject, XmlWriter>, IAixmConverterMetadata>>();

        public AixmConverter()
        {
            List<Assembly> source = AppDomain.CurrentDomain.GetAssemblies().ToList<Assembly>();
            foreach (var asm in source)
            {
                AssemblyCatalog catalog = new AssemblyCatalog(asm);
                CompositionContainer mefContainer = new CompositionContainer(catalog, true);
                var plugins = mefContainer.GetExports<Func<IAixmConverter, JObject, XElement, IEnumerable<JObject>>, IAixmConverterMetadata>().ToArray();
                _elementReaders.AddRange(plugins);
                _elementWriters.AddRange(mefContainer.GetExports<Action<IAixmConverter,int, JObject, XmlWriter>, IAixmConverterMetadata>().ToArray());

            }
            _elementWriters = _elementWriters.OrderBy(k => k.Metadata.WriteOrder).ToList();
        }
        public Newtonsoft.Json.Linq.JArray ReadElements(JObject currentArray, IEnumerable<System.Xml.Linq.XElement> enumerable)
        {
            var array = new JArray();
            foreach (var element in enumerable)
            {
                var objs = ReadElement(currentArray, element);
                foreach (var obj in objs)
                {
                    if(obj["geometry"] == null)
                    {
                        Console.WriteLine("WARNING: Element did not have geometry, adding empty.");
                        Console.WriteLine(obj.ToString());
                        obj.SetGeometry(new JObject());

                    }
                    array.Add(obj);

                }
            }

            return array;
        }
        public IEnumerable<JObject> ReadElement(JObject currentObject, XElement element)
        {
            var converters = _elementReaders.Where(k => k.Metadata.AIXMElementName == element.Name.LocalName).ToList();
            if (converters.Any())
            {
               
               return converters.First().Value(this, currentObject, element);
            }
            else
            {

                Console.WriteLine(element.Name);
            }   
            return Enumerable.Empty<JObject>();

           // return new AIXMBasicMessageConvertor().ReadElement(this, new JObject(), element);
        }






        public IEnumerable<JObject> ReadElement(JObject currentObject, IEnumerable<XElement> enumerable)
        {
            var any = false;
            foreach (var element in enumerable)
            {
                
                var objs = ReadElement(currentObject, element);
                foreach(var obj in objs)
                {
                    any = true;
                    yield return obj;
                }
            }
            if(!any)
                yield return currentObject;
        }

        public void WriteElement(JObject obj, System.IO.Stream fs)
        {
            using (var writer = XmlWriter.Create(fs, new XmlWriterSettings
            {
                 Indent = true,
            }))
            {
                writer.WriteStartDocument(true);
                
                writer.WriteStartElement("message", "AIXMBasicMessage", "http://www.aixm.aero/schema/5.1/message");
//                writer.WriteStartAttribute("gml");//http://www.aixm.aero/schema/5.1/message
                writer.WriteAttributeString("xmlns", "gml", null, "http://www.opengis.net/gml/3.2");
                writer.WriteAttributeString("xmlns", "aixm", null, "http://www.aixm.aero/schema/5.1");

                foreach(JObject feature in obj["features"])
                {
                    var props = feature["properties"];
                    if (props != null)
                    {

                        for (int i = 0; i < _elementWriters.Count;i++ )
                        {
                            _elementWriters[i].Value(this, 0, feature, writer);
                        }
                        for (int i = _elementWriters.Count-1; i >=0; i--)
                        {
                            _elementWriters[i].Value(this, 1, feature, writer);
                        }
                    }
                }

          //      writer.WriteEndAttribute();
                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
        }
    }
}

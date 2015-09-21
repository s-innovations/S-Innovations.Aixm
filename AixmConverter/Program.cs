using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AixmConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var doc = XDocument.Load(@"C:\dev\aixm\Chicago_OHare_BASELINE\Chicago_OHare_BASELINE.xml");

            var converter = new SInnovations.Aixm.AixmConverter();
            var obj = converter.ReadElement(new JObject(), doc.Root).First();
          
            using (var fs = new StreamWriter(new FileStream(@"C:\dev\aixm\Chicago_OHare_BASELINE\Chicago_OHare_BASELINE.geojson", FileMode.Create)))
            {
                using (var writer = new JsonTextWriter(fs))
                {
                    obj.WriteTo(writer);
                }
            }
             using (Stream fs =new FileStream(@"C:\dev\aixm\Chicago_OHare_BASELINE\Chicago_OHare_BASELINE.aixm5", FileMode.Create))
            {

                converter.WriteElement(obj, fs);

             }

             Console.WriteLine(File.ReadAllText(@"C:\dev\aixm\Chicago_OHare_BASELINE\Chicago_OHare_BASELINE.aixm5"));
        }
    }
}

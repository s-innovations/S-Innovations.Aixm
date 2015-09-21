using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SInnovations.Aixm.Extensions
{
    [MetadataAttribute]
  
    class AixmConverterMetadataAttribute : Attribute, IAixmConverterMetadata
    {
        public string AIXMVersion
        {
            get;
            set;
        }

        public string AIXMElementName
        {
            get;
            set;
        }

        public string AIXMElementScheme
        {
            get;
            set;
        }


        public int WriteOrder
        {
            get;
            set;
        }
    }
    
   
}

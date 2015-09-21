using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SInnovations.Aixm.Extensions
{
    public interface IAixmConverterMetadata
    {
        string AIXMVersion { get; }
        string AIXMElementName { get; }
        string AIXMElementScheme { get; }

        int WriteOrder { get;}
    }
}

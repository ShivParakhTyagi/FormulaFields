using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class GeoControl : FieldsInfo
    {
        public GeoControl()
        {
            MobilizeType = "GeoControl";
            TypeStructure = "Longitude,Latitude";
        }
    }
}

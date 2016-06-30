using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class GeoStamp : FieldsInfo
    {
        public GeoStamp()
        {
            MobilizeType = "GeoStamp";
            TypeStructure = "Longitude,Latitude";
        }
        public bool Visible { get; set; } = true;
    }
}

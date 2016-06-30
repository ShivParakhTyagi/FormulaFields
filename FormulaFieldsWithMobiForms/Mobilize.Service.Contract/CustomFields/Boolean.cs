using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class Boolean:FieldsInfo
    {
        public Boolean()
        {
            MobilizeType = "Boolean";
        }
        public bool DefaultValue { get; set; }
        //public bool EnforceUniqueValues { get; set; }
    }
}

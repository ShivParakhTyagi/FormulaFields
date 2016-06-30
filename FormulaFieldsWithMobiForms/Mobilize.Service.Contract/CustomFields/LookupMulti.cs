using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.MobilizeDataTypes;

namespace Mobilize.Contract.CustomFields
{
    public class LookupMulti : FieldsInfo
    {
        public LookupMulti()
        {
            MobilizeType = "LookupMulti";
        }

        public bool EnforceUniqueValues { get; set; }
        public string FormName { get; set; }
        public string FieldName { get; set; }
        //the following fields though are part of the contract are not to used by client.. these fields will be populated through code internally..
        public string LookupField { get; set; }
        public string LookupList { get; set; }
        public string Query { get; set; }

    }
}

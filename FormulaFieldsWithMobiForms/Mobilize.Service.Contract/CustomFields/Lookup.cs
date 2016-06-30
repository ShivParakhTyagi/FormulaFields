using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class Lookup : FieldsInfo
    {
        public Lookup()
        {
            MobilizeType = "Lookup";
        }

        public bool EnforceUniqueValues { get; set; }
        //public string DefaultValue { get; set; }
        //public bool ReadOnlyField { get; set; }
        //public bool AllowMultipleValues { get; set; }
        //public bool IsRelationship { get; set; }
        public string FormName { get; set; }
        public string FieldName { get; set; }
        //public bool AllowMultipleValues { get; set; }
        //the following fields though are part of the contract are not to used by client.. these fields will be populated through code internally..
        public string LookupField { get; set; }
        public string LookupList { get; set; }
        public string Query { get; set; }

    }
}

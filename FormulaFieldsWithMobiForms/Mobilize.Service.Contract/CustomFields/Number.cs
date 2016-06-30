using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class Number:FieldsInfo
    {
        public Number()
        {
            MobilizeType = "Number";
        }
        public bool EnforceUniqueValues { get; set; }
        public string MinimumValue { get; set; }
        public string MaximumValue { get; set; }
        public string MinimumMaximumValueErrorMessage { get; set; }

        public string DefaultValue { get; set; }
    }
}

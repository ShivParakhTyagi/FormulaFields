using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class Text : FieldsInfo
    {
        public Text()
        {
            MobilizeType = "Text";
        }
        public string DefaultValue { get; set; }
        public string MaxLength { get; set; }
        public string MaxLengthErrorMessage { get; set; }
        public bool EnforceUniqueValues { get; set; }
    }
}

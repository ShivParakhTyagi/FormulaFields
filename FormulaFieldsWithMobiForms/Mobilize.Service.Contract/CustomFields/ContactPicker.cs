using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class ContactPicker : FieldsInfo
    {
        public ContactPicker()
        {
            MobilizeType = "ContactPicker";
        }
        public string DefaultValue { get; set; }
        public bool EnforceUniqueValues { get; set; }
        public string MaxLength { get; set; }
        public string MaxLengthErrorMessage { get; set; }
        public bool LimitToList { get; set; } = false;
        public string ContactPickerLabel { get; set; } = "select contact";
    }
}

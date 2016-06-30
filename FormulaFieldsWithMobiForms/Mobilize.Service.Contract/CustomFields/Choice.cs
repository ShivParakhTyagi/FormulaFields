using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class Choice : FieldsInfo
    {
        public Choice()
        {
            MobilizeType = "Choice";
        }
        public string DefaultValue { get; set; }
        //public bool ReadOnlyField { get; set; }
        //public bool EnforceUniqueValues { get; set; }// to be deleted
        //public bool FillInChoice { get; set; }
        //public string Mappings { get; set; }
        public string Choices { get; set; }
    }
}

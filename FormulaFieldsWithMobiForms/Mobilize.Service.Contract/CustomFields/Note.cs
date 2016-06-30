using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class Note: FieldsInfo
    {
        public Note()
        {
            MobilizeType = "Note";
        }
        public int NumberOfLines { get; set; }
        //public bool RichText { get; set; }
    }
}

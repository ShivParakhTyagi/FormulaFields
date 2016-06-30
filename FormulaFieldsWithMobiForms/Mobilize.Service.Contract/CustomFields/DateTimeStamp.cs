using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class DateTimeStamp:FieldsInfo
    {
        public DateTimeStamp()
        {
            MobilizeType = "DateTimeStamp";
        }

        public bool Visible { get; set; } = true;
        //public string MinTime { get; set; }
        //public string MaxTime { get; set; }
    }
}

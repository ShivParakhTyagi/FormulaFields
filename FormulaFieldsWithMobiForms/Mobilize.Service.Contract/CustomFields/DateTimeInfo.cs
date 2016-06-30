using Mobilize.Contract.MobilizeDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CustomFields
{
    public class DateTimeInfo: FieldsInfo
    {
        public DateTimeInfo()
        {
            MobilizeType = "DateTimeInfo";
        }
        public bool EnforceUniqueValues { get; set; }
        public bool DateOnly { get; set; }
        //public bool ReadOnlyField { get; set; }
        public string DefaultValue { get; set; }
        //TODO: to be included once the feature is complete at Mobi App...  also changes to be done in validation class
        //public string MinTime { get; set; }
        //public string MaxTime { get; set; }
    }
}

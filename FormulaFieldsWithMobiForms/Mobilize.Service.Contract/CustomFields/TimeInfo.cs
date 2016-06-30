using Mobilize.Contract.MobilizeDataTypes;

namespace Mobilize.Contract.CustomFields
{
    public class TimeInfo : FieldsInfo
    {
        public TimeInfo()
        {
            MobilizeType = "TimeInfo";
        }
        public string DefaultValue { get; set; }
        public bool EnforceUniqueValues { get; set; }
    }
}
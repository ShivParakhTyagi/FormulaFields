using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.MobilizeDataTypes;
using Newtonsoft.Json;

namespace Mobilize.Contract.CustomFields
{
    public class SubForm : FieldsInfo
    {
        public SubForm()
        {
            MobilizeType = "SubForm";
        }

        /// <summary>
        /// user friendly name of sub mobi form
        /// </summary>
        public string SubMobiForm { get; set; }
        /// <summary>
        /// internal name of the sub mobi form
        /// </summary>
        public string SubMobiFormInternalName { get; set; }
        public string SubFormMode { get; set; }

        //[JsonIgnore]
        public MobiForm MobiForm { get; set; }

        //[JsonIgnore]
        public List<FieldsInfo> AllFields
        {
            get
            {
                var list = new List<FieldsInfo>();
                if (MobiForm?.Pages == null || MobiForm?.AllFields == null)
                {
                    return list;
                }
                list.AddRange(MobiForm.AllFields);
                return list;
            }
        }
    }
}

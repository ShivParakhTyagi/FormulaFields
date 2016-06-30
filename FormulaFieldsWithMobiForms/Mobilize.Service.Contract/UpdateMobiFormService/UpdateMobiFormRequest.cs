using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.MobilizeDataTypes;

namespace Mobilize.Contract.UpdateMobiFormService
{
    public class UpdateMobiFormRequest
    {
        public string MobiFormJson { get; set; }
        public string DeletedFields { get; set; }
    }
}

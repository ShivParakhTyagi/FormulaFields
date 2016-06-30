using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Mobilize.Contract.SyncListItemsService
{
    public class SyncListItemsRequest
    {
        public SyncForm SyncFormData { get; set; }
        public string FormName { get; set; }
    }
    public class SyncForm
    {
        public string FormName { get; set; }
        public List<JObject> Data { get; set; }
    }
}

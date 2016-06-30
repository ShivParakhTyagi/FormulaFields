using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.ServerListItemsService
{
    public class ServerListItemsResponse : ServiceResponse
    {
        public string Data { get; set; }
    }

    public class ServerLookupItems
    {
        public string FormName { get; set; }
        public Dictionary<string, List<LookupItemValue>> Data { get; set; }
    }

    public class LookupItemValue
    {
        public int Id { get; set; }
        public string Value { get; set; }
    }
}

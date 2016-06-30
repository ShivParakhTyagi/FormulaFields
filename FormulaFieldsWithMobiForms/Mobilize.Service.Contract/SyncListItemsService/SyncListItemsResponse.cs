using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.CreateListItemService;

namespace Mobilize.Contract.SyncListItemsService
{
    public class SyncListItemsResponse : ServiceResponse
    {
        public List<SyncedItems> Data { get; set; }
    }
    public class SyncedItems
    {
        public ResponseFormItem ResponseItem { get; set; }
        public Guid ClientId { get; set; } = Guid.Empty;
        public string ServerId { get; set; } = string.Empty;
        public string State { get; set; }
        public bool Success { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.UpdateListItemService
{
    public class UpdateListItemRequest
    {
        public string ListName { get; set; }
        public string ItemId { get; set; }
        public string ValueString { get; set; }
    }
}

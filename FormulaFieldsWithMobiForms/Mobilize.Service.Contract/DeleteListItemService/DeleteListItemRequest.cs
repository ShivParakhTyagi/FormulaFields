using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Mobilize.Contract.DeleteListItemService
{
    public class DeleteListItemRequest
    {
        public string ListName { get; set; }
        public string ItemId { get; set; }
        public string ValueString { get; set; }
    }
}

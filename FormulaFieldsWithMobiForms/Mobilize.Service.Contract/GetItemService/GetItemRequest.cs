using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetItemService
{
    public class GetItemRequest
    {
        public string ListName { get; set; }
        public string FormatType { get; set; }
        public string ItemId { get; set; }
    }
}

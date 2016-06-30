using System;
using Newtonsoft.Json.Linq;

namespace Mobilize.Contract.CreateListItemService
{
    public class CreateListItemRequest
    {
        public string ListName { get; set; }
        public string ValueString { get; set; }
    }

    public class SubFormListItem
    {
        public int? Id { get; set; }
        public Guid? ClientId { get; set; }
        public string Flag { get; set; }
        public JObject ValueString { get; set; }
    }
}
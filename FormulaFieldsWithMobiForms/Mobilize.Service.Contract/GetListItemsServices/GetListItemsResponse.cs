using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetListItemsServices
{
    public class GetListItemsResponse : ServiceResponse
    {
        public string ListItemsJson { get; set; }
    }

    public class FieldInformation
    {
        public string InternalName { get; set; }
        public string Label { get; set; }
        public string Type { get; set; }
        public bool Sortable { get; set; }
        public bool Filterable { get; set; }
        public bool Hidden { get; set; }
        public string FilterType { get; set; }
        public bool ShowInListView { get; set; }
        public string SubForm { get; set; }
        public string SubFormMode { get; set; }

        public string Formula { get; set; }

        public string FullName { get; set; }

        public List<string> ImpactedFields { get; set; }

        public List<FieldInformation> SubFormFields { get; set; }

        public Dictionary<string, object> AdditionalInformation { get; set; }
    }
}

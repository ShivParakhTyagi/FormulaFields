using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.CreateListItemService;

namespace Mobilize.Contract.UpdateListItemService
{
    public class UpdateListItemResponse : ServiceResponse
    {
        public string ItemId { get; set; }
        public ResponseFormItem UpdatedFormItem { get; set; }
    }
}

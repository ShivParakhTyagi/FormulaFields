using System;
using System.Collections.Generic;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.CreateListItemService
{
    public class CreateListItemResponse : ServiceResponse
    {
        public string ItemId { get; set; }
        public ResponseFormItem CreatedFormItem { get; set; }
    }
    
    public class ResponseFormItem
    {
        public string Flag { get; set; }
        public Guid? Guid { get; set; }
        public int Id { get; set; }
        public List<SubFormItem> SubFormItems { get; set; }
    }

    public class SubFormItem
    {
        public string FieldInternalName { get; set; }
        public List<ResponseFormItem> ResponseFormItems { get; set; }
    }
}

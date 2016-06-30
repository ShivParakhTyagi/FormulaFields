using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetItemService
{
    public class GetItemResponse : ServiceResponse
    {
        public string Layout { get; set; }
        public string DataJson { get; set; }
    }
}

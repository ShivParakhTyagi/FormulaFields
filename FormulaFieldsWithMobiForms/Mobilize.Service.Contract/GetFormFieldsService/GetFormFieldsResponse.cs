using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetFormFieldsService
{
    public class GetFormFieldsResponse: ServiceResponse
    {
        public string ListOfFields { get; set; }
    }
}

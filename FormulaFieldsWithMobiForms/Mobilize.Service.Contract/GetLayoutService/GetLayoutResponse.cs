using Mobilize.Contract.AssignedFormsService;
using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetLayoutService
{
    public class GetLayoutResponse : ServiceResponse
    {
        public List<Form> ListOfLayouts { get; set; }
    }
}

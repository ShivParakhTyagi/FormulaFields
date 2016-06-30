using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.GetMobiFormService
{
    public class GetMobiFormResponse :ServiceResponse
    {
        public string MobiFormJsonString { get; set; }
    }
}

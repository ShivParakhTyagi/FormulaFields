using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.MobiFormExistsService
{
    public class MobiFormExistsResponse: ServiceResponse
    {
        public bool Exists { get; set; }
    }
}

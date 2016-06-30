using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.CheckLoginIdService
{
    public class CheckLoginIdResponse : ServiceResponse
    {
        public bool Available { get; set; }
    }
}

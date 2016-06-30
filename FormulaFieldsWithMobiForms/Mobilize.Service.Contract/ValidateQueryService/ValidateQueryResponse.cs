using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mobilize.Contract.Base;

namespace Mobilize.Contract.ValidateQueryService
{
    public class ValidateQueryResponse : ServiceResponse
    {
        public bool IsValid { get; set; }
    }
}

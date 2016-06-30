using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.ResetPasswordService
{
    public class ResetPasswordResponse: ServiceResponse
    {
        public string UserId { get; set; }
        public string NewPassword { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.AdminLoginService
{
    public class AdminLoginRequest
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }
}

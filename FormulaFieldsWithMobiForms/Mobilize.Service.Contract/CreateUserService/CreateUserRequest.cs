using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.CreateUserService
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNo { get; set; }
        public string LoginId { get; set; }
        public string LoginPassword { get; set; }
        public List<string> RoleIds { get; set; }
    }
}

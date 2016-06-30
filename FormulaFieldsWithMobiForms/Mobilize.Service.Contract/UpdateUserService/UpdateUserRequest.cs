using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.UpdateUserService
{
    public class UpdateUserRequest
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string CountryCode { get; set; }
        public List<string> RoleIds { get; set; }
        public bool IsEnabled { get; set; }
    }
}

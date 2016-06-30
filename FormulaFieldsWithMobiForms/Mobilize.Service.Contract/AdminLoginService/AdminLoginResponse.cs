using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.AdminLoginService
{
    public class AdminLoginResponse : ServiceResponse
    {
        public bool Authenticated { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPhoneNo { get; set; }
        public string UserArea { get; set; }
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string RoleIds { get; set; }
        public List<string> Forms { get; set; }
    }
}

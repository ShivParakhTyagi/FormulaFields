using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetRolesService
{
    public class GetRolesResponse : ServiceResponse
    {
        public List<Role> Roles { get; set; }
    }
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<string> Forms { get; set; }
    }
}

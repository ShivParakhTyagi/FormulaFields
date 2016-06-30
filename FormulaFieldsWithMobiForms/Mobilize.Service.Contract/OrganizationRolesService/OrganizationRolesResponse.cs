using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.OrganizationRolesService
{
    public class OrganizationRolesResponse : ServiceResponse
    {
        //public List<Role> Roles { get; set; }
        public string RoleId { get; set; }
    }
}

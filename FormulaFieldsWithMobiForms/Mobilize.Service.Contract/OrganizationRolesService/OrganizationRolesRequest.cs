using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.OrganizationRolesService
{
    public class OrganizationRolesRequest
    {
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<string> Forms { get; set; }
    }
}

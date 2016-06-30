﻿using Mobilize.Contract.OrganizationRolesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.UpdateRoleService
{
    public class UpdateRoleRequest
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<string> Forms { get; set; }
    }
}

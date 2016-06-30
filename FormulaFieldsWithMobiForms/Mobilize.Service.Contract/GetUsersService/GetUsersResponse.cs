using Mobilize.Contract.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobilize.Contract.GetUsersService
{
    public class GetUsersResponse : ServiceResponse
    {
        public List<User> Users { get; set; }
    }
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LoginId { get; set; }
        public string CountryCode { get; set; }
        public string PhoneNo { get; set; }
        public int OrganizationId { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsAdmin { get; set; }
        public string OrganizationName { get; set; }
        public List<UserRole> Roles { get; set; }
    }

    public class UserRole
    {
        public int RoleId { get; set ; }
        public string RoleName { get; set ; }
}
}

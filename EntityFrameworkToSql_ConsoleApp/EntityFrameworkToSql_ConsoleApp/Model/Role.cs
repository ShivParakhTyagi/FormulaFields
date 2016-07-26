using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class Role
    {
        public Role()
        {
        }
        public int RoleId { get; set; }
        public string RoleDescription { get; set; }
        public string RoleName { get; set; }
        [ForeignKey("RoleOrganization")]
        public int? OrganizationId { get; set; }
        public virtual Organization RoleOrganization { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
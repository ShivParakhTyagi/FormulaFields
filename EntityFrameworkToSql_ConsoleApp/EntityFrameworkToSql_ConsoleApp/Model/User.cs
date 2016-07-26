using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class User
    {
        public User()
        {
            this.UserRoles = new HashSet<Role>();
        }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string CountryCode { get; set; }
        public string PhoneNo { get; set; }
        public bool IsAdmin { get; set; }

        //public string DeviceIMEINumber { get; set; }
        public bool CanSavePassword { get; set; }

        public bool IsDisabled { get; set; }
        [ForeignKey("UserOrganization")]
        public int? OrganizationId { get; set; }
        public virtual Login  UserLogin{ get; set; }
        public virtual Organization UserOrganization { get; set; }
        public virtual ICollection<Role> UserRoles { get; set; }

    }
}
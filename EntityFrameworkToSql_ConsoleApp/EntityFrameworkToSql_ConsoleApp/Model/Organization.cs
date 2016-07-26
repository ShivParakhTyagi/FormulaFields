using System.Collections.Generic;

namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class Organization
    {
        public int OrganizationId { get; set; }
        public string OrganizationName { get; set; }
        public string SharePointUrl { get; set; }
        public string SiteUserName { get; set; }
        public string SitePassword { get; set; }
        public string SQLConnectionString { get; set; }
        public bool InSQL { get; set; }
        public ICollection<Role> Roles { get; set; }
    }
}
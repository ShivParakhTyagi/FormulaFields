using System.Data.Entity;

namespace EntityFrameworkToSql_ConsoleApp.Model
{
    public class MobilizeDbSchema : DbContext
    {
        public MobilizeDbSchema() : base("name=DefaultConnection")
        {

        }

        public new virtual Database Database
        {
            get { return this.Database; }
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SupportRequests> SupportRequests { get; set; }
        public virtual DbSet<SupportType> SupportType { get; set; }
        public virtual DbSet<FileAttachments> FileAttachments { get; set; }

        public virtual DbSet<SysDocuments> SysDocuments { get; set; }

        public virtual DbSet<UserToken> UserTokens { get; set; }
    }
}
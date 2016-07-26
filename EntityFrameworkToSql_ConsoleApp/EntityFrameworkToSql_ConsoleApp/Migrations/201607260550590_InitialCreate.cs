namespace EntityFrameworkToSql_ConsoleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileAttachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Size = c.Long(nullable: false),
                        Content = c.Binary(),
                        SupportRequests_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SupportRequests", t => t.SupportRequests_Id)
                .Index(t => t.SupportRequests_Id);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        LoginId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.LoginId);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        OrganizationId = c.Int(nullable: false, identity: true),
                        OrganizationName = c.String(),
                        SharePointUrl = c.String(),
                        SiteUserName = c.String(),
                        SitePassword = c.String(),
                        SQLConnectionString = c.String(),
                        InSQL = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleDescription = c.String(),
                        RoleName = c.String(),
                        OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.RoleId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        CountryCode = c.String(),
                        PhoneNo = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        CanSavePassword = c.Boolean(nullable: false),
                        IsDisabled = c.Boolean(nullable: false),
                        OrganizationId = c.Int(),
                        UserLogin_LoginId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Logins", t => t.UserLogin_LoginId)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId)
                .Index(t => t.OrganizationId)
                .Index(t => t.UserLogin_LoginId);
            
            CreateTable(
                "dbo.SupportRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Phone = c.String(),
                        PreferredTime = c.DateTime(nullable: false),
                        ModeOfSupport = c.String(),
                        Description = c.String(),
                        TypeOfSupport_Id = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SupportTypes", t => t.TypeOfSupport_Id)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.TypeOfSupport_Id)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.SupportTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SysDocuments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        FormName = c.String(),
                        Data = c.String(),
                        ReadOnly = c.Boolean(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedBy = c.String(),
                        OrganizationId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.OrganizationId)
                .Index(t => t.OrganizationId);
            
            CreateTable(
                "dbo.UserTokens",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LoginId = c.String(),
                        DeviceId = c.String(),
                        ExpiryTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SysDocuments", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.SupportRequests", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.SupportRequests", "TypeOfSupport_Id", "dbo.SupportTypes");
            DropForeignKey("dbo.FileAttachments", "SupportRequests_Id", "dbo.SupportRequests");
            DropForeignKey("dbo.UserRoles", "Role_RoleId", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "OrganizationId", "dbo.Organizations");
            DropForeignKey("dbo.Users", "UserLogin_LoginId", "dbo.Logins");
            DropForeignKey("dbo.Roles", "OrganizationId", "dbo.Organizations");
            DropIndex("dbo.UserRoles", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRoles", new[] { "User_UserId" });
            DropIndex("dbo.SysDocuments", new[] { "OrganizationId" });
            DropIndex("dbo.SupportRequests", new[] { "User_UserId" });
            DropIndex("dbo.SupportRequests", new[] { "TypeOfSupport_Id" });
            DropIndex("dbo.Users", new[] { "UserLogin_LoginId" });
            DropIndex("dbo.Users", new[] { "OrganizationId" });
            DropIndex("dbo.Roles", new[] { "OrganizationId" });
            DropIndex("dbo.FileAttachments", new[] { "SupportRequests_Id" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserTokens");
            DropTable("dbo.SysDocuments");
            DropTable("dbo.SupportTypes");
            DropTable("dbo.SupportRequests");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Organizations");
            DropTable("dbo.Logins");
            DropTable("dbo.FileAttachments");
        }
    }
}

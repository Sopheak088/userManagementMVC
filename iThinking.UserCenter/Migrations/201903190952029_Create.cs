namespace iThinking.UserCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationErrors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        StackTrace = c.String(),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetGroupRoles",
                c => new
                    {
                        ApplicationRoleId = c.String(nullable: false, maxLength: 128),
                        ApplicationGroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationRoleId, t.ApplicationGroupId })
                .ForeignKey("dbo.AspNetGroups", t => t.ApplicationGroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.ApplicationRoleId, cascadeDelete: true)
                .Index(t => t.ApplicationRoleId)
                .Index(t => t.ApplicationGroupId);
            
            CreateTable(
                "dbo.AspNetGroups",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Description = c.String(),
                        ApplicationProjectId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetProjects", t => t.ApplicationProjectId, cascadeDelete: true)
                .Index(t => t.ApplicationProjectId);
            
            CreateTable(
                "dbo.AspNetProjects",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserGroupChanges",
                c => new
                    {
                        ApplicationUserChangeId = c.Guid(nullable: false),
                        ApplicationGroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUserChangeId, t.ApplicationGroupId })
                .ForeignKey("dbo.AspNetGroups", t => t.ApplicationGroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUserChanges", t => t.ApplicationUserChangeId, cascadeDelete: true)
                .Index(t => t.ApplicationUserChangeId)
                .Index(t => t.ApplicationGroupId);
            
            CreateTable(
                "dbo.AspNetUserChanges",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationUserId = c.String(nullable: false),
                        ReasonChange = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Birthday = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        AvatarPath = c.String(),
                        UploadFolder = c.String(),
                        Points = c.Int(),
                        CountViews = c.Int(),
                        About = c.String(),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserGroupHistories",
                c => new
                    {
                        ApplicationUserHistoryId = c.Guid(nullable: false),
                        ApplicationGroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUserHistoryId, t.ApplicationGroupId })
                .ForeignKey("dbo.AspNetGroups", t => t.ApplicationGroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUserHistories", t => t.ApplicationUserHistoryId, cascadeDelete: true)
                .Index(t => t.ApplicationUserHistoryId)
                .Index(t => t.ApplicationGroupId);
            
            CreateTable(
                "dbo.AspNetUserHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ApplicationUserId = c.String(nullable: false),
                        ApplicationUserChangeId = c.Guid(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Birthday = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        AvatarPath = c.String(),
                        UploadFolder = c.String(),
                        Points = c.Int(),
                        CountViews = c.Int(),
                        About = c.String(),
                        CreatedBy = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedBy = c.String(maxLength: 256),
                        UpdatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUserChanges", t => t.ApplicationUserChangeId)
                .Index(t => t.ApplicationUserChangeId);
            
            CreateTable(
                "dbo.AspNetUserGroups",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        ApplicationGroupId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.ApplicationGroupId })
                .ForeignKey("dbo.AspNetGroups", t => t.ApplicationGroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ApplicationGroupId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(maxLength: 256),
                        LastName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Birthday = c.DateTime(),
                        Gender = c.Int(nullable: false),
                        AvatarPath = c.String(),
                        UploadFolder = c.String(nullable: false),
                        Points = c.Int(),
                        CountViews = c.Int(),
                        About = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        ApplicationUserChangeId = c.Guid(),
                        CreatedDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUserChanges", t => t.ApplicationUserChangeId)
                .Index(t => t.ApplicationUserChangeId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false, maxLength: 250),
                        GroupName = c.String(),
                        ApplicationProjectId = c.String(maxLength: 128),
                        Description = c.String(maxLength: 250),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetProjects", t => t.ApplicationProjectId)
                .Index(t => t.ApplicationProjectId)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetGroupRoles", "ApplicationRoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserGroups", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetRoles", "ApplicationProjectId", "dbo.AspNetProjects");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ApplicationUserChangeId", "dbo.AspNetUserChanges");
            DropForeignKey("dbo.AspNetUserGroups", "ApplicationGroupId", "dbo.AspNetGroups");
            DropForeignKey("dbo.AspNetUserGroupHistories", "ApplicationUserHistoryId", "dbo.AspNetUserHistories");
            DropForeignKey("dbo.AspNetUserHistories", "ApplicationUserChangeId", "dbo.AspNetUserChanges");
            DropForeignKey("dbo.AspNetUserGroupHistories", "ApplicationGroupId", "dbo.AspNetGroups");
            DropForeignKey("dbo.AspNetUserGroupChanges", "ApplicationUserChangeId", "dbo.AspNetUserChanges");
            DropForeignKey("dbo.AspNetUserGroupChanges", "ApplicationGroupId", "dbo.AspNetGroups");
            DropForeignKey("dbo.AspNetGroupRoles", "ApplicationGroupId", "dbo.AspNetGroups");
            DropForeignKey("dbo.AspNetGroups", "ApplicationProjectId", "dbo.AspNetProjects");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetRoles", new[] { "ApplicationProjectId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "ApplicationUserChangeId" });
            DropIndex("dbo.AspNetUserGroups", new[] { "ApplicationGroupId" });
            DropIndex("dbo.AspNetUserGroups", new[] { "ApplicationUserId" });
            DropIndex("dbo.AspNetUserHistories", new[] { "ApplicationUserChangeId" });
            DropIndex("dbo.AspNetUserGroupHistories", new[] { "ApplicationGroupId" });
            DropIndex("dbo.AspNetUserGroupHistories", new[] { "ApplicationUserHistoryId" });
            DropIndex("dbo.AspNetUserGroupChanges", new[] { "ApplicationGroupId" });
            DropIndex("dbo.AspNetUserGroupChanges", new[] { "ApplicationUserChangeId" });
            DropIndex("dbo.AspNetGroups", new[] { "ApplicationProjectId" });
            DropIndex("dbo.AspNetGroupRoles", new[] { "ApplicationGroupId" });
            DropIndex("dbo.AspNetGroupRoles", new[] { "ApplicationRoleId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserGroups");
            DropTable("dbo.AspNetUserHistories");
            DropTable("dbo.AspNetUserGroupHistories");
            DropTable("dbo.AspNetUserChanges");
            DropTable("dbo.AspNetUserGroupChanges");
            DropTable("dbo.AspNetProjects");
            DropTable("dbo.AspNetGroups");
            DropTable("dbo.AspNetGroupRoles");
            DropTable("dbo.ApplicationErrors");
        }
    }
}

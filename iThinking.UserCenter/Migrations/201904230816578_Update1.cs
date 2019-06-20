namespace iThinking.UserCenter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUserChanges", "IsCanLogin", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUserHistories", "IsCanLogin", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "IsCanLogin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsCanLogin");
            DropColumn("dbo.AspNetUserHistories", "IsCanLogin");
            DropColumn("dbo.AspNetUserChanges", "IsCanLogin");
        }
    }
}

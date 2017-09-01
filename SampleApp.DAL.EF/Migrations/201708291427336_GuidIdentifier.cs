namespace SampleApp.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GuidIdentifier : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Tasks");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Tasks", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Tasks", "Id");
            AddPrimaryKey("dbo.Users", "Id");
            AddForeignKey("dbo.Tasks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "UserId", "dbo.Users");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Tasks");
            AlterColumn("dbo.Users", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Tasks", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Users", "Id");
            AddPrimaryKey("dbo.Tasks", "Id");
            AddForeignKey("dbo.Tasks", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}

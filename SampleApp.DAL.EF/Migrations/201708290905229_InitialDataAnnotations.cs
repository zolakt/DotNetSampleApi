namespace SampleApp.DAL.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDataAnnotations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "Name", c => c.String(maxLength: 255));
            AlterColumn("dbo.Users", "FirstName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "LastName", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "Country", c => c.String(maxLength: 20));
            AlterColumn("dbo.Users", "City", c => c.String(maxLength: 100));
            AlterColumn("dbo.Users", "Street", c => c.String(maxLength: 255));
            AlterColumn("dbo.Users", "HouseNumber", c => c.String(maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "HouseNumber", c => c.String());
            AlterColumn("dbo.Users", "Street", c => c.String());
            AlterColumn("dbo.Users", "City", c => c.String());
            AlterColumn("dbo.Users", "Country", c => c.String());
            AlterColumn("dbo.Users", "LastName", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Tasks", "Name", c => c.String());
        }
    }
}

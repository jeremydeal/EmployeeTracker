namespace EmployeeTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeModel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "ImageMimeType", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "ImageMimeType");
        }
    }
}

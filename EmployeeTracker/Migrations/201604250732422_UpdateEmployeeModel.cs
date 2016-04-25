namespace EmployeeTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateEmployeeModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "State", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "State", c => c.String(nullable: false, maxLength: 2));
        }
    }
}

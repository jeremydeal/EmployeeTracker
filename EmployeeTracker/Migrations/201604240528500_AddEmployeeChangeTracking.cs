namespace EmployeeTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmployeeChangeTracking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeChangeHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EmployeeID = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        OldValue = c.String(nullable: false),
                        NewValue = c.String(nullable: false),
                        DateChanged = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeChangeHistories");
        }
    }
}

namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHasPasswordSetToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "HasPasswordSet", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "HasPasswordSet");
        }
    }
}

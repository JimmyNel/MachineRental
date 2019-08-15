namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetEmailRequiredToMachinist : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Machinists", "Email", c => c.String(nullable: false, maxLength: 255));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Machinists", "Email", c => c.String(maxLength: 255));
        }
    }
}

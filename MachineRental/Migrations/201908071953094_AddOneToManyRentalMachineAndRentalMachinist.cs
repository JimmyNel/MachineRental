namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToManyRentalMachineAndRentalMachinist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "MachineId", c => c.Int(nullable: false));
            AddColumn("dbo.Rentals", "MachinistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "MachineId");
            CreateIndex("dbo.Rentals", "MachinistId");
            AddForeignKey("dbo.Rentals", "MachineId", "dbo.Machines", "Id");
            AddForeignKey("dbo.Rentals", "MachinistId", "dbo.Machinists", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "MachinistId", "dbo.Machinists");
            DropForeignKey("dbo.Rentals", "MachineId", "dbo.Machines");
            DropIndex("dbo.Rentals", new[] { "MachinistId" });
            DropIndex("dbo.Rentals", new[] { "MachineId" });
            DropColumn("dbo.Rentals", "MachinistId");
            DropColumn("dbo.Rentals", "MachineId");
        }
    }
}

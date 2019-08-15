namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddParkNumberAndMachineTypeToMachine : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Machines", "MachineTypeId", c => c.Byte(nullable: false));
            AddColumn("dbo.Machines", "ParkNumberId", c => c.Byte(nullable: false));
            CreateIndex("dbo.Machines", "MachineTypeId");
            CreateIndex("dbo.Machines", "ParkNumberId");
            AddForeignKey("dbo.Machines", "MachineTypeId", "dbo.MachineTypes", "Id");
            AddForeignKey("dbo.Machines", "ParkNumberId", "dbo.ParkNumbers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Machines", "ParkNumberId", "dbo.ParkNumbers");
            DropForeignKey("dbo.Machines", "MachineTypeId", "dbo.MachineTypes");
            DropIndex("dbo.Machines", new[] { "ParkNumberId" });
            DropIndex("dbo.Machines", new[] { "MachineTypeId" });
            DropColumn("dbo.Machines", "ParkNumberId");
            DropColumn("dbo.Machines", "MachineTypeId");
        }
    }
}

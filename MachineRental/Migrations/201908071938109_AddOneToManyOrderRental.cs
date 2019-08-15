namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToManyOrderRental : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rentals", "OrderId", c => c.Int(nullable: false));
            CreateIndex("dbo.Rentals", "OrderId");
            AddForeignKey("dbo.Rentals", "OrderId", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rentals", "OrderId", "dbo.Orders");
            DropIndex("dbo.Rentals", new[] { "OrderId" });
            DropColumn("dbo.Rentals", "OrderId");
        }
    }
}

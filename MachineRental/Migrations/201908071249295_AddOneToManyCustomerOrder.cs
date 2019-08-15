namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToManyCustomerOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropColumn("dbo.Orders", "CustomerId");
        }
    }
}

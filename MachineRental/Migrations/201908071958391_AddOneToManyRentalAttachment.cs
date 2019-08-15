namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToManyRentalAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "RentalId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attachments", "RentalId");
            AddForeignKey("dbo.Attachments", "RentalId", "dbo.Rentals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "RentalId", "dbo.Rentals");
            DropIndex("dbo.Attachments", new[] { "RentalId" });
            DropColumn("dbo.Attachments", "RentalId");
        }
    }
}

namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSignatureImgToAttachment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachments", "SignatureImgId", c => c.Int(nullable: false));
            CreateIndex("dbo.Attachments", "SignatureImgId");
            AddForeignKey("dbo.Attachments", "SignatureImgId", "dbo.SignatureImgs", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "SignatureImgId", "dbo.SignatureImgs");
            DropIndex("dbo.Attachments", new[] { "SignatureImgId" });
            DropColumn("dbo.Attachments", "SignatureImgId");
        }
    }
}

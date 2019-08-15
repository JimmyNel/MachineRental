namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValuesToMachineType : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[MachineTypes] ([Id], [Name]) VALUES (1, N'Pelleteuse à chenilles')
                    INSERT INTO [dbo].[MachineTypes] ([Id], [Name]) VALUES (2, N'Mécalac')
                    INSERT INTO [dbo].[MachineTypes] ([Id], [Name]) VALUES (3, N'Trax')
                    INSERT INTO [dbo].[MachineTypes] ([Id], [Name]) VALUES (4, N'Pelle à pneus')
                    INSERT INTO [dbo].[MachineTypes] ([Id], [Name]) VALUES (5, N'Divers')");
        }
        
        public override void Down()
        {
        }
    }
}

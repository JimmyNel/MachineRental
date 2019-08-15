namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValuesToParkNumber : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (1, N'200')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (2, N'201')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (3, N'202')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (4, N'300')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (5, N'301')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (6, N'302')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (7, N'400')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (8, N'401')
                    INSERT INTO [dbo].[ParkNumbers] ([Id], [Number]) VALUES (9, N'402')");
        }
        
        public override void Down()
        {
        }
    }
}

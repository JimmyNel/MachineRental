namespace MachineRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsersAndRoles : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsEnabled]) VALUES (N'adb1bb42-3dd9-463b-911d-a2bd11849ffc', N'rentalmanager@gpoisson.fr', 0, N'AOUyCK5cIvGOl8RyDM97liCd76vqek5iJUwjQ9cpxXgvtGB+K789pVBrSoTr6xe5qA==', N'86549104-c36c-4573-b29a-6ebf3e8ca422', NULL, 0, 0, NULL, 1, 0, N'rentalmanager@gpoisson.fr', 1)
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsEnabled]) VALUES (N'fc0c4abf-55a4-4a7b-abb0-bf51aaeaaad8', N'machinist@gpoisson.fr', 0, N'AOyUOYmWH77UYETNn9GXy7g0IYX2gw7JbdNvwIbndzrliYicoDz33J4FLbUTzLmx6w==', N'6ed77f48-3113-41f7-aac7-9638e0bc9dbc', NULL, 0, 0, NULL, 1, 0, N'machinist@gpoisson.fr', 1)
            INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [IsEnabled]) VALUES (N'fd43fbe4-6586-41e6-97ee-864fce0e7d3e', N'admin@gpoisson.fr', 0, N'ABX4zIKFlCrH7YQHXz1CSgpLddrxGzwGDbI2Y57T7qWQ8yexc7A3pi3g48BI2xkPhA==', N'5cb75ee0-d79d-4876-8094-311d419fbe00', NULL, 0, 0, NULL, 1, 0, N'admin@gpoisson.fr', 1)
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a60302d9-7e1d-4945-b855-e87112a9b4e4', N'Admin')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'b22e7555-8446-4d9e-a740-b3530c349e0d', N'Machinist')
            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'6b3eb74a-e279-44fb-b64a-13defaa786d7', N'RentalManager')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'adb1bb42-3dd9-463b-911d-a2bd11849ffc', N'6b3eb74a-e279-44fb-b64a-13defaa786d7')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fd43fbe4-6586-41e6-97ee-864fce0e7d3e', N'a60302d9-7e1d-4945-b855-e87112a9b4e4')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'fc0c4abf-55a4-4a7b-abb0-bf51aaeaaad8', N'b22e7555-8446-4d9e-a740-b3530c349e0d')
            ");
        }
        
        public override void Down()
        {
        }
    }
}

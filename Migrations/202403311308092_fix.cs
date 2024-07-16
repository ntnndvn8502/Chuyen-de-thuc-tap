namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserRealName = c.String(),
                        UserEmail = c.String(),
                        UserPhone = c.String(),
                        UserStatus = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
    }
}

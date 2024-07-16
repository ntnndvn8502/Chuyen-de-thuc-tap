namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        RtrID = c.Int(nullable: false),
                        Arrival = c.DateTime(nullable: false),
                        BookTime = c.DateTime(nullable: false),
                        OrderNote = c.String(),
                        OrdStatus = c.String(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.Restaurants", t => t.RtrID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID)
                .Index(t => t.RtrID);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RtrID = c.Int(nullable: false, identity: true),
                        RtrRealName = c.String(),
                        RtrEmail = c.String(),
                        RtrPhone = c.String(),
                        RtrAddress = c.String(),
                        RtrDesc = c.String(),
                        OpenTime = c.DateTime(nullable: false),
                        CloseTime = c.DateTime(nullable: false),
                        RtrStatus = c.String(),
                    })
                .PrimaryKey(t => t.RtrID);
            
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
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropForeignKey("dbo.Orders", "RtrID", "dbo.Restaurants");
            DropIndex("dbo.Orders", new[] { "RtrID" });
            DropIndex("dbo.Orders", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Orders");
        }
    }
}

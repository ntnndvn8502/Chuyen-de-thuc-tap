namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "UserID", "dbo.Users");
            DropIndex("dbo.Orders", new[] { "UserID" });
            AddColumn("dbo.Orders", "ArrivalDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "ArrivalTime", c => c.Time(nullable: false, precision: 7));
            DropColumn("dbo.Orders", "UserID");
            DropColumn("dbo.Orders", "Arrival");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Arrival", c => c.DateTime(nullable: false));
            AddColumn("dbo.Orders", "UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Orders", "ArrivalTime");
            DropColumn("dbo.Orders", "ArrivalDate");
            CreateIndex("dbo.Orders", "UserID");
            AddForeignKey("dbo.Orders", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}

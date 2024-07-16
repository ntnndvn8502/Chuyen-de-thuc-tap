namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerID = c.String(nullable: false, maxLength: 128),
                        CustomerUserName = c.String(),
                        CustomerRealName = c.String(),
                        CustomerEmail = c.String(),
                        CustomerPhone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            AddColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            AddColumn("dbo.Orders", "UserAction", c => c.String());
            AddColumn("dbo.Orders", "RtrAction", c => c.String());
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerID");
            DropColumn("dbo.Orders", "OrdStatus");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "OrdStatus", c => c.String());
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropColumn("dbo.Orders", "RtrAction");
            DropColumn("dbo.Orders", "UserAction");
            DropColumn("dbo.Orders", "CustomerId");
            DropTable("dbo.Customers");
        }
    }
}

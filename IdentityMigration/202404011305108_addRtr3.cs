namespace Simple.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRtr3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "RtrID", "dbo.Restaurants");
            DropIndex("dbo.AspNetUsers", new[] { "RtrID" });
            AlterColumn("dbo.AspNetUsers", "RtrID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "RtrID");
            AddForeignKey("dbo.AspNetUsers", "RtrID", "dbo.Restaurants", "RtrID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RtrID", "dbo.Restaurants");
            DropIndex("dbo.AspNetUsers", new[] { "RtrID" });
            AlterColumn("dbo.AspNetUsers", "RtrID", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "RtrID");
            AddForeignKey("dbo.AspNetUsers", "RtrID", "dbo.Restaurants", "RtrID", cascadeDelete: true);
        }
    }
}

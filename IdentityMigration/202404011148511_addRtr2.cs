namespace Simple.IdentityMigration
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRtr2 : DbMigration
    {
        public override void Up()
        {
           
            
            AddColumn("dbo.AspNetUsers", "RtrID", c => c.Int(nullable: true));
            CreateIndex("dbo.AspNetUsers", "RtrID");
            AddForeignKey("dbo.AspNetUsers", "RtrID", "dbo.Restaurants", "RtrID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RtrID", "dbo.Restaurants");
            DropIndex("dbo.AspNetUsers", new[] { "RtrID" });
            DropColumn("dbo.AspNetUsers", "RtrID");
            DropTable("dbo.Restaurants");
        }
    }
}

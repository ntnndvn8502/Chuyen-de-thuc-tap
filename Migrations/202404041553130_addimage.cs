namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageID = c.Int(nullable: false, identity: true),
                        ImageUrl = c.String(),
                        RtrID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ImageID)
                .ForeignKey("dbo.Restaurants", t => t.RtrID, cascadeDelete: true)
                .Index(t => t.RtrID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "RtrID", "dbo.Restaurants");
            DropIndex("dbo.Images", new[] { "RtrID" });
            DropTable("dbo.Images");
        }
    }
}

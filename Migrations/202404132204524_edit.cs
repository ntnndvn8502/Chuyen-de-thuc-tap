namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "RtrEmail", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "RtrEmail", c => c.String(nullable: false));
        }
    }
}

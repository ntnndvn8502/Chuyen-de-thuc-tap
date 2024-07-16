namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "RtrRealName", c => c.String(nullable: false));
            AlterColumn("dbo.Restaurants", "RtrEmail", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "RtrEmail", c => c.String());
            AlterColumn("dbo.Restaurants", "RtrRealName", c => c.String());
        }
    }
}

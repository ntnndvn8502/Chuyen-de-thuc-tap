namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRtr : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Restaurants", "RtrCity", c => c.String());
            AddColumn("dbo.Restaurants", "RtrAvatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restaurants", "RtrAvatar");
            DropColumn("dbo.Restaurants", "RtrCity");
        }
    }
}

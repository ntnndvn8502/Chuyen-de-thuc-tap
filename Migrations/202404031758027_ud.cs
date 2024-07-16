namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ud : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "RtrMessage", c => c.String());
            AddColumn("dbo.Orders", "Happen", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Happen");
            DropColumn("dbo.Orders", "RtrMessage");
        }
    }
}

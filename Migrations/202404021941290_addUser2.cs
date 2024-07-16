namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUser2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Personal", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Contact");
            DropColumn("dbo.Orders", "Personal");
        }
    }
}

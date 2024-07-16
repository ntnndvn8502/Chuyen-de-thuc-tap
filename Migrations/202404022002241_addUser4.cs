namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUser4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Orders", "Personal");
            DropColumn("dbo.Orders", "Contact");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Contact", c => c.String());
            AddColumn("dbo.Orders", "Personal", c => c.Boolean(nullable: false));
        }
    }
}

namespace Simple.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTimeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Restaurants", "OpenTime", c => c.Time(nullable: false, precision: 7));
            AlterColumn("dbo.Restaurants", "CloseTime", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Restaurants", "CloseTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Restaurants", "OpenTime", c => c.DateTime(nullable: false));
        }
    }
}

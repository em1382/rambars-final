namespace Rambars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bars", "AddressLineTwo", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Bars", "AddressLineTwo", c => c.String(nullable: false));
        }
    }
}

namespace Rambars.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mig69 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Specials", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Specials", "Description");
        }
    }
}

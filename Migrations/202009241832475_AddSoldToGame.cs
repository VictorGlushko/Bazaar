namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSoldToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Sold", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Sold");
        }
    }
}

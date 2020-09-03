namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOriginalAndFinalPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "OriginalPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "FinalPrice", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "FinalPrice");
            DropColumn("dbo.Games", "OriginalPrice");
        }
    }
}

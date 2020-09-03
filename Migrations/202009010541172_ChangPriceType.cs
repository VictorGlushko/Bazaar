namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangPriceType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}

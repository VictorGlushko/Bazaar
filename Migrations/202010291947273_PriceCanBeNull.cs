namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PriceCanBeNull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarouselSlides", "Price", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarouselSlides", "Price", c => c.Int(nullable: false));
        }
    }
}

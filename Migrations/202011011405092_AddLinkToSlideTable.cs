namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLinkToSlideTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarouselSlides", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarouselSlides", "Link");
        }
    }
}

namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnToCarouelSlideTabetAndDelGame : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CarouselSlides", "Game_Id", "dbo.Games");
            DropIndex("dbo.CarouselSlides", new[] { "Game_Id" });
            AddColumn("dbo.CarouselSlides", "ImgPath", c => c.String());
            AddColumn("dbo.CarouselSlides", "Price", c => c.Int(nullable: false));
            DropColumn("dbo.CarouselSlides", "Game_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CarouselSlides", "Game_Id", c => c.Int());
            DropColumn("dbo.CarouselSlides", "Price");
            DropColumn("dbo.CarouselSlides", "ImgPath");
            CreateIndex("dbo.CarouselSlides", "Game_Id");
            AddForeignKey("dbo.CarouselSlides", "Game_Id", "dbo.Games", "Id");
        }
    }
}

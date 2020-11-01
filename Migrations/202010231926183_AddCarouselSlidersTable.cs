namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarouselSlidersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarouselSlides",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Description = c.String(),
                        Title = c.String(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarouselSlides", "Game_Id", "dbo.Games");
            DropIndex("dbo.CarouselSlides", new[] { "Game_Id" });
            DropTable("dbo.CarouselSlides");
        }
    }
}

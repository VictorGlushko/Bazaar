namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Platforms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlatformGames",
                c => new
                    {
                        Platform_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Platform_Id, t.Game_Id })
                .ForeignKey("dbo.Platforms", t => t.Platform_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Platform_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformGames", "Platform_Id", "dbo.Platforms");
            DropIndex("dbo.PlatformGames", new[] { "Game_Id" });
            DropIndex("dbo.PlatformGames", new[] { "Platform_Id" });
            DropTable("dbo.PlatformGames");
            DropTable("dbo.Platforms");
        }
    }
}

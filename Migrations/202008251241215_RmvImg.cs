namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RmvImg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Id", "dbo.Images");
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TagGames", "Game_Id", "dbo.Games");
            DropIndex("dbo.Games", new[] { "Id" });
            DropPrimaryKey("dbo.Games");
            AlterColumn("dbo.Games", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Games", "Id");
            AddForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PreviewImagePath = c.String(),
                        MainImagePath = c.String(),
                        GameId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TagGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropPrimaryKey("dbo.Games");
            AlterColumn("dbo.Games", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Games", "Id");
            CreateIndex("dbo.Games", "Id");
            AddForeignKey("dbo.TagGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "Id", "dbo.Images", "Id");
        }
    }
}

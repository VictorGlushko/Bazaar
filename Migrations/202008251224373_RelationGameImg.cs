namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelationGameImg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TagGames", "Game_Id", "dbo.Games");
            DropPrimaryKey("dbo.Games");
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
            
            AlterColumn("dbo.Games", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Games", "Id");
            CreateIndex("dbo.Games", "Id");
            AddForeignKey("dbo.Games", "Id", "dbo.Images", "Id");
            AddForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Games", "Id", "dbo.Images");
            DropIndex("dbo.Games", new[] { "Id" });
            DropPrimaryKey("dbo.Games");
            AlterColumn("dbo.Games", "Id", c => c.Int(nullable: false, identity: true));
            DropTable("dbo.Images");
            AddPrimaryKey("dbo.Games", "Id");
            AddForeignKey("dbo.TagGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.PlatformGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
        }
    }
}

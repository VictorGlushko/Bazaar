namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveGameMode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.ModeGames",
                    c => new
                    {
                        Mode_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Mode_Id, t.Game_Id });

            CreateTable(
                    "dbo.Modes",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            CreateIndex("dbo.ModeGames", "Game_Id");
            CreateIndex("dbo.ModeGames", "Mode_Id");
            AddForeignKey("dbo.ModeGames", "Game_Id", "dbo.Games", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ModeGames", "Mode_Id", "dbo.Modes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
          
        }
    }
}

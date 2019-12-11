namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTAgTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagGames",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Game_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.TagGames", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagGames", new[] { "Game_Id" });
            DropIndex("dbo.TagGames", new[] { "Tag_Id" });
            DropTable("dbo.TagGames");
            DropTable("dbo.Tags");
        }
    }
}

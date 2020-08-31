namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OneToOneGameImg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ImagePaths", "Image_Id", "dbo.Images");
            RenameColumn(table: "dbo.ImagePaths", name: "Image_Id", newName: "Image_GameId");
            RenameIndex(table: "dbo.ImagePaths", name: "IX_Image_Id", newName: "IX_Image_GameId");
            DropPrimaryKey("dbo.Images");
            AddColumn("dbo.Images", "GameId", c => c.Int(nullable: false));
            AlterColumn("dbo.Images", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Images", "GameId");
            CreateIndex("dbo.Images", "GameId");
            AddForeignKey("dbo.Images", "GameId", "dbo.Games", "Id");
            AddForeignKey("dbo.ImagePaths", "Image_GameId", "dbo.Images", "GameId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImagePaths", "Image_GameId", "dbo.Images");
            DropForeignKey("dbo.Images", "GameId", "dbo.Games");
            DropIndex("dbo.Images", new[] { "GameId" });
            DropPrimaryKey("dbo.Images");
            AlterColumn("dbo.Images", "Id", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.Images", "GameId");
            AddPrimaryKey("dbo.Images", "Id");
            RenameIndex(table: "dbo.ImagePaths", name: "IX_Image_GameId", newName: "IX_Image_Id");
            RenameColumn(table: "dbo.ImagePaths", name: "Image_GameId", newName: "Image_Id");
            AddForeignKey("dbo.ImagePaths", "Image_Id", "dbo.Images", "Id");
        }
    }
}

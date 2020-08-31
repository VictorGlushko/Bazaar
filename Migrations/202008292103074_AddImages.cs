namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImages : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Images", name: "GameId", newName: "ImageId");
            RenameColumn(table: "dbo.ImagePaths", name: "Image_GameId", newName: "Image_ImageId");
            RenameIndex(table: "dbo.Images", name: "IX_GameId", newName: "IX_ImageId");
            RenameIndex(table: "dbo.ImagePaths", name: "IX_Image_GameId", newName: "IX_Image_ImageId");
            DropColumn("dbo.Images", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Images", "Id", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.ImagePaths", name: "IX_Image_ImageId", newName: "IX_Image_GameId");
            RenameIndex(table: "dbo.Images", name: "IX_ImageId", newName: "IX_GameId");
            RenameColumn(table: "dbo.ImagePaths", name: "Image_ImageId", newName: "Image_GameId");
            RenameColumn(table: "dbo.Images", name: "ImageId", newName: "GameId");
        }
    }
}

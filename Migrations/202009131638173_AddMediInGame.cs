namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMediInGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MediaResources",
                c => new
                    {
                        MediaResourcesId = c.Int(nullable: false),
                        MainImagePath = c.String(nullable: false),
                        PreviewImagePath = c.String(),
                        GalleryPath = c.String(nullable: false),
                        VideoLink = c.String(),
                    })
                .PrimaryKey(t => t.MediaResourcesId)
                .ForeignKey("dbo.Games", t => t.MediaResourcesId)
                .Index(t => t.MediaResourcesId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MediaResources", "MediaResourcesId", "dbo.Games");
            DropIndex("dbo.MediaResources", new[] { "MediaResourcesId" });
            DropTable("dbo.MediaResources");
        }
    }
}

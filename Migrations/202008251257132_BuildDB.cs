namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BuildDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImagePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Image_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Images", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImagePaths", "Image_Id", "dbo.Images");
            DropIndex("dbo.ImagePaths", new[] { "Image_Id" });
            DropTable("dbo.ImagePaths");
        }
    }
}

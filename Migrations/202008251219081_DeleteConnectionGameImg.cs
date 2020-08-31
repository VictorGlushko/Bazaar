namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteConnectionGameImg : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "ImageId", "dbo.Images");
            DropIndex("dbo.Games", new[] { "ImageId" });
            DropColumn("dbo.Games", "ImageId");
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
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Games", "ImageId", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "ImageId");
            AddForeignKey("dbo.Games", "ImageId", "dbo.Images", "Id", cascadeDelete: true);
        }
    }
}

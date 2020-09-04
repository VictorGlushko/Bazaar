namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGalleryPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "GalleryPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "GalleryPath");
        }
    }
}

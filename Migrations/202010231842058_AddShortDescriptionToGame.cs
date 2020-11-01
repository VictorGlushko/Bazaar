namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShortDescriptionToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "ShortDescription");
        }
    }
}

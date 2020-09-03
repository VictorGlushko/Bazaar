namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSlugToGameTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Slag", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Slag");
        }
    }
}

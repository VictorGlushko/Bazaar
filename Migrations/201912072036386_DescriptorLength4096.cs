namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescriptorLength4096 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "Description", c => c.String(nullable: false, maxLength: 2048));
        }
    }
}

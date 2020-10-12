namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeTypeOfOrderInFaqTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FaqItems", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FaqItems", "Order", c => c.String());
        }
    }
}

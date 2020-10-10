namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMaxLenToFaqItem : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FaqItems", "Question", c => c.String(nullable: false));
            AlterColumn("dbo.FaqItems", "Answer", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FaqItems", "Answer", c => c.String());
            AlterColumn("dbo.FaqItems", "Question", c => c.String());
        }
    }
}

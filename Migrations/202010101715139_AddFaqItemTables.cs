namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFaqItemTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FaqItems",
                c => new
                    {
                        FaqItemId = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Answer = c.String(),
                        Order = c.String(),
                    })
                .PrimaryKey(t => t.FaqItemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FaqItems");
        }
    }
}

namespace Bazaar.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSysReqTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SystemRequirements",
                c => new
                    {
                        SystemRequirementsId = c.Int(nullable: false),
                        OperatingSystem = c.String(nullable: false),
                        Processor = c.String(nullable: false),
                        RAM = c.String(nullable: false),
                        VideoCard = c.String(nullable: false),
                        HDD = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.SystemRequirementsId)
                .ForeignKey("dbo.Games", t => t.SystemRequirementsId)
                .Index(t => t.SystemRequirementsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SystemRequirements", "SystemRequirementsId", "dbo.Games");
            DropIndex("dbo.SystemRequirements", new[] { "SystemRequirementsId" });
            DropTable("dbo.SystemRequirements");
        }
    }
}

namespace AccountManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TitheTableAddition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tithe",
                c => new
                    {
                        TitheId = c.Int(nullable: false, identity: true),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        IncomeId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.TitheId)
                .ForeignKey("dbo.Income", t => t.IncomeId, cascadeDelete: true)
                .Index(t => t.IncomeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tithe", "IncomeId", "dbo.Income");
            DropIndex("dbo.Tithe", new[] { "IncomeId" });
            DropTable("dbo.Tithe");
        }
    }
}

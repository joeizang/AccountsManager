namespace AccountManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BankChange : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bank", "AccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bank", "AccountId", c => c.Int(nullable: false));
        }
    }
}

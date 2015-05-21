namespace AccountManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccountEnumType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "AccountTypes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "AccountTypes");
        }
    }
}

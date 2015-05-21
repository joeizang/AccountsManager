namespace AccountManager.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialdatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        AccountsId = c.Int(nullable: false, identity: true),
                        AccountName = c.String(nullable: false, unicode: false),
                        AccountBalance = c.Decimal(precision: 18, scale: 2),
                        BankId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AccountsId)
                .ForeignKey("dbo.Bank", t => t.BankId, cascadeDelete: true)
                .Index(t => t.BankId);
            
            CreateTable(
                "dbo.Bank",
                c => new
                    {
                        BankId = c.Int(nullable: false, identity: true),
                        BankName = c.String(nullable: false, unicode: false),
                        AccountId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BankId);
            
            CreateTable(
                "dbo.Expense",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false, identity: true),
                        PayerId = c.Int(nullable: false),
                        ExpenseCategoryId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        Description = c.String(unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ExpenseId)
                .ForeignKey("dbo.ExpenseCategory", t => t.ExpenseCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Payer", t => t.PayerId, cascadeDelete: true)
                .ForeignKey("dbo.Receiver", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => t.PayerId)
                .Index(t => t.ExpenseCategoryId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.ExpenseCategory",
                c => new
                    {
                        ExpenseCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ExpenseCategoryId);
            
            CreateTable(
                "dbo.Payer",
                c => new
                    {
                        PayerId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.PayerId);
            
            CreateTable(
                "dbo.Receiver",
                c => new
                    {
                        ReceiverId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Address = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Income",
                c => new
                    {
                        IncomeId = c.Int(nullable: false, identity: true),
                        PayerId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        Tithe = c.Decimal(precision: 18, scale: 2),
                        IncomeCategoryId = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(unicode: false),
                        Date = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.IncomeId)
                .ForeignKey("dbo.IncomeCategory", t => t.IncomeCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Payer", t => t.PayerId, cascadeDelete: true)
                .ForeignKey("dbo.Receiver", t => t.ReceiverId, cascadeDelete: true)
                .Index(t => t.PayerId)
                .Index(t => t.ReceiverId)
                .Index(t => t.IncomeCategoryId);
            
            CreateTable(
                "dbo.IncomeCategory",
                c => new
                    {
                        IncomeCategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        Description = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.IncomeCategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Income", "ReceiverId", "dbo.Receiver");
            DropForeignKey("dbo.Income", "PayerId", "dbo.Payer");
            DropForeignKey("dbo.Income", "IncomeCategoryId", "dbo.IncomeCategory");
            DropForeignKey("dbo.Expense", "ReceiverId", "dbo.Receiver");
            DropForeignKey("dbo.Expense", "PayerId", "dbo.Payer");
            DropForeignKey("dbo.Expense", "ExpenseCategoryId", "dbo.ExpenseCategory");
            DropForeignKey("dbo.Accounts", "BankId", "dbo.Bank");
            DropIndex("dbo.Income", new[] { "IncomeCategoryId" });
            DropIndex("dbo.Income", new[] { "ReceiverId" });
            DropIndex("dbo.Income", new[] { "PayerId" });
            DropIndex("dbo.Expense", new[] { "ReceiverId" });
            DropIndex("dbo.Expense", new[] { "ExpenseCategoryId" });
            DropIndex("dbo.Expense", new[] { "PayerId" });
            DropIndex("dbo.Accounts", new[] { "BankId" });
            DropTable("dbo.IncomeCategory");
            DropTable("dbo.Income");
            DropTable("dbo.Receiver");
            DropTable("dbo.Payer");
            DropTable("dbo.ExpenseCategory");
            DropTable("dbo.Expense");
            DropTable("dbo.Bank");
            DropTable("dbo.Accounts");
        }
    }
}

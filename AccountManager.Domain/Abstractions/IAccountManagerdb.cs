using System;
using System.Data.Entity;
using AccountManager.Domain.Entities;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
namespace AccountManager.Domain.Abstractions
{
    public interface IAccountManagerdb : IDisposable
    {
        IDbSet<Accounts> Accounts { get; set; }
        IDbSet<Bank> Bank { get; set; }
        IDbSet<Expense> Expense { get; set; }
        IDbSet<ExpenseCategory> ExpenseCategory { get; set; }
        IDbSet<Income> Income { get; set; }
        IDbSet<IncomeCategory> IncomeCategory { get; set; }
        IDbSet<Payer> Payer { get; set; }
        IDbSet<Receiver> Receiver { get; set; }
        DbEntityEntry Entry(object o);
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}

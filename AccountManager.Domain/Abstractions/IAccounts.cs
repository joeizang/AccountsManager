using System;
using AccountManager.Domain.Entities;
namespace AccountManager.Domain.Abstractions
{
    public interface IAccounts
    {
        decimal? AccountBalance { get; set; }
        string AccountName { get; set; }
        int AccountsId { get; set; }
        Bank Bank { get; set; }
        int BankId { get; set; }
    }
}

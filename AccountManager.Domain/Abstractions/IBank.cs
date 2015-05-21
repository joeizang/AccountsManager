using System;
using System.Collections.Generic;
using AccountManager.Domain.Entities;

namespace AccountManager.Domain.Abstractions
{
    public interface IBank
    {
        ICollection<Accounts> Accounts { get; set; }
        int BankId { get; set; }
        string BankName { get; set; }
    }
}

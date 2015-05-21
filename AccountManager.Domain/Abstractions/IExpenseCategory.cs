using System;
namespace AccountManager.Domain.Abstractions
{
    interface IExpenseCategory
    {
        string Description { get; set; }
        int ExpenseCategoryId { get; set; }
        string Name { get; set; }
    }
}

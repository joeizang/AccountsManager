using System;
namespace AccountManager.Domain.Abstractions
{
    interface IIncomeCategory
    {
        string Description { get; set; }
        int IncomeCategoryId { get; set; }
        string Name { get; set; }
    }
}

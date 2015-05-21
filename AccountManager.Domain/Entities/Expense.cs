using AccountManager.Domain.Abstractions;
using AccountManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain.Entities
{
    public class Expense : IExpense
    {
        [Key]
        public int ExpenseId { get; set; }

        public int PayerId { get; set; }

        public virtual Payer Payer { get; set; }

        [Display(Name="Expense Category")]
        public int ExpenseCategoryId { get; set; }

        public virtual ExpenseCategory ExpenseCategory { get; set; }

        public int ReceiverId { get; set; }

        public virtual Receiver Receiver { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal Amount { get; set; }
    }
}

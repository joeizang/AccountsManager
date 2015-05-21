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
    public class Income : IIncome
    {
        [Key]
        public int IncomeId { get; set; }

        public int PayerId { get; set; }

        //when adding a payer to an income entry, if the payer is not in the dropdown then a link to create a payer should appear at the end
        [Display(Name="Payer")]
        public virtual Payer Payer { get; set; }

        public int ReceiverId { get; set; }

        [Display(Name="Receiver")]
        public virtual Receiver Receiver { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal? Tithe { get; set; }

        public int IncomeCategoryId { get; set; }

        [Display(Name="Income Category")]
        public virtual IncomeCategory IncomeCategory { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal Amount { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}

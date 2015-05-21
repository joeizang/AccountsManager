using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManager.Domain.Entities;
using AccountManager.Domain.Abstractions;

namespace AccountManager.Domain.Entities
{
    /// <summary>
    /// Every Account belongs in one unique bank and should show account balance at any time
    /// Account should not have any logic in it only to display a balance. Any kind of logic 
    /// should be in a business object for calculating account balances and other related tasks.
    /// </summary>
    public class Accounts : IAccounts
    {
        [Key]
        public int AccountsId { get; set; }
        [Required]
        [Display(Name="Account Name")]
        public string AccountName { get; set; }

        [EnumDataType(typeof(AccountTypes))]
        public AccountTypes AccountTypes { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name="Account Balance")]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal? AccountBalance { get; set; }
        [Display(Name="Bank")]
        public int BankId { get; set; }
        public virtual Bank Bank { get; set; }
        
    }
}

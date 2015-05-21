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
    public class Bank : IBank
    {
        [Key]
        public int BankId { get; set; }
        [Required]
        [Display(Name="Bank Name")]
        public string BankName { get; set; }
        //public int AccountId { get; set; }
        public virtual ICollection<Accounts> Accounts { get; set; }
        
        
    }
}

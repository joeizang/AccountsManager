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
    public class IncomeCategory : IIncomeCategory
    {
        public int IncomeCategoryId { get; set; }
        
        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}

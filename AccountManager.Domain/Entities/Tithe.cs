using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain.Entities
{
    public class Tithe
    {
        [Key]
        public int TitheId { get; set; }
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal? Amount { get; set; }
        public int IncomeId { get; set; }
        public Income Income { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString="{0:D}")]
        public DateTime Date { get; set; }
    }
}

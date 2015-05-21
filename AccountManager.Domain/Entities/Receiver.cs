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
    public class Receiver : IReceiver
    {
        [Key]
        public int ReceiverId { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage="The Name cannot be more than 50 Letters!")]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

    }
}

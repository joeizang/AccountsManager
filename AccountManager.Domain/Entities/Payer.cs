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
    public class Payer : IPayer
    {
        [Key]
        public int PayerId { get; set; }

        [DataType(DataType.Text)]
        [MaxLength(50,ErrorMessage="Name Cannot be longer than 50 Letters!")]
        public string Name { get; set; }


    }
}

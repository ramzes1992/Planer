using Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planer.Models
{
    public class AccountModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Amount { get; set; }

        public AccountType Type { get; set; }
        
    }
}

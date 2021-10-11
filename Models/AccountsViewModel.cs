using eshop.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class AccountsViewModel
    {
        public IList<User> Accounts { get; set; }
    }
}

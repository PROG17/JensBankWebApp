using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JensBankWebApp.Models;

namespace JensBankWebApp.ViewModels
{
    public class DepositWithdrawViewModel
    {
        public int AccountNo { get; set; }
        public decimal Amount { get; set; }
        public Account Account { get; set; }
        public string Message { get; set; }
    }
}

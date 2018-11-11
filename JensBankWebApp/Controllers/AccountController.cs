using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JensBankWebApp.ViewModels;
using JensBankWebApp.Models;

namespace JensBankWebApp.Controllers
{
    public class AccountController : Controller
    {
        private IBankRepository _bankrepo;

        public AccountController(IBankRepository bankRepo)
        {
            _bankrepo = bankRepo;
        }

        public IActionResult DepositWithdraw()
        {
            return View(new DepositWithdrawViewModel());
        }

        [HttpPost]
        public IActionResult DepositWithdraw(DepositWithdrawViewModel model, int choice)
        {
            if (choice == 1)
                return View(_bankrepo.Deposit(model));
            else
                return View(_bankrepo.Withdraw(model));
        }
    }
}
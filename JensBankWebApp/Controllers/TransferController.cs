using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JensBankWebApp.Models;
using JensBankWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace JensBankWebApp.Controllers
{
    public class TransferController : Controller
    {

        private IBankRepository _bankrepo;

        public TransferController(IBankRepository bankRepo)
        {
            _bankrepo = bankRepo;
        }


        public IActionResult MakeAMoneyTransfer()
        {
            return View(new TransferViewModel());
        }


        [HttpPost]
        public IActionResult MakeAMoneyTransfer(TransferViewModel model)
        {
            if (!IsModelValid(model))
                return View(nameof(MakeAMoneyTransfer), model);

            var origin = _bankrepo.GetAccountById(model.OriginAccountId);

            var destination = _bankrepo.GetAccountById(model.DestinationAccountId);

            string message = null;
            var status = _bankrepo.MoneyTransfer(origin, destination, model.TransferAmount, out message);

            model.Message = message;
            model.IsError = true;

            if (status)
            {
                model.IsError = false;
                model.OriginNewAmount = origin.Amount;
                model.DestinationNewAmount = destination.Amount;
            }

            return View(nameof(MakeAMoneyTransfer), model);
        }


        private bool IsModelValid(TransferViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Message = "Error: Operation cannot be processed due to errors in input fields";

                model.IsError = true;

                return false;
            }
            return true;
        }

    }
}
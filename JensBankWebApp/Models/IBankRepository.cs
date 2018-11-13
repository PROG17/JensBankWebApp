using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JensBankWebApp.ViewModels;

namespace JensBankWebApp.Models
{
    public interface IBankRepository
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        Account GetAccountById(int id);
        DepositWithdrawViewModel Deposit(DepositWithdrawViewModel model);
        DepositWithdrawViewModel Withdraw(DepositWithdrawViewModel model);

        bool MoneyTransfer(Account origin, Account destination, decimal amount, out string message);
    }
}

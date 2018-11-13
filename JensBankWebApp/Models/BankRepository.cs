using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JensBankWebApp.ViewModels;

namespace JensBankWebApp.Models
{
    public class BankRepository : IBankRepository
    {
        private List<Customer> _customers;
        private List<Account> _accounts;

        public BankRepository()
        {
            PopulateCustomersAndAccounts();
            PopulateAccountList();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Account GetAccountById(int id)
        {
            return _accounts.FirstOrDefault(a => a.Id == id);
        }

        public Customer GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(c => c.Id == id);
        }

        public DepositWithdrawViewModel Deposit(DepositWithdrawViewModel model)
        {
            model.Message = null;
            model.Account = null;

            var account = _accounts.FirstOrDefault(c => c.Id == model.AccountNo);

            if (account != null)
            {
                if (model.Amount > 0)
                {
                    account.Amount += model.Amount;
                    model.Account = account;
                    model.Message = "Deposit completed";
                }
                else
                    model.Message = "Amount has to be digits with a value above 0";
            }
            else
                model.Message = "Invalid account number";

            return model;
        }

        public DepositWithdrawViewModel Withdraw(DepositWithdrawViewModel model)
        {
            model.Message = null;
            model.Account = null;

            var account = _accounts.FirstOrDefault(c => c.Id == model.AccountNo);

            if (account != null)
            {
                if (model.Amount <= account.Amount && model.Amount > 0)
                {
                    account.Amount -= model.Amount;
                    model.Account = account;
                    model.Message = "Withdraw completed";
                }
                else if (model.Amount < 1)
                    model.Message = "Amount has to be digits with a value above 0";
                else
                    model.Message = "Not enough money on account";
            }
            else
                model.Message = "Invalid account number";

            return model;
        }


        public bool MoneyTransfer(Account origin, Account destination, decimal amount, out string message)
        {
            if (origin == null || destination == null)
            {
                message = "Error: Invalid origin and/or destination account";

                return false;
            }

            if (origin.Id == destination.Id)
            {
                message = "Error: Money transfer is possible only between different account numbers";

                return false;
            }

            if (amount <= 0)
            {
                message = "Error: Invalid amount. It must be larger than 0";

                return false;
            }

            if (amount > origin.Amount)
            {
                message = "Error: Amount is larger than origin´s account balance";

                return false;
            }

            origin.Amount -= amount;

            destination.Amount += amount;

            message = $"Success: Transferred {amount} kr from account nr. {origin.Id} to account nr. {destination.Id}";

            return true;
        }

        private void PopulateAccountList()
        {
            var list = new List<Account>();

            foreach (var cust in _customers)
            {
                list.AddRange(cust.Accounts);
            }

            _accounts = list;
        }

        private void PopulateCustomersAndAccounts()
        {
            _customers = CreateCustomers();
        }

        private List<Customer> CreateCustomers()
        {
            return new List<Customer>
            {
                new Customer
                {
                    Id=1,
                    Name = "Customer 1",
                    Accounts = CreateAccounts(1)
                },
                new Customer
                {
                    Id=2,
                    Name = "Customer 2",
                    Accounts = CreateAccounts(2)
                },
                new Customer
                {
                    Id=3,
                    Name = "Customer 3",
                    Accounts = CreateAccounts(3)
                },
                new Customer
                {
                    Id=4,
                    Name = "Customer 4",
                    Accounts = CreateAccounts(4)
                }
            };
        }

        private List<Account> CreateAccounts(int id)
        {
            return new List<Account>
            {
                new Account
                {
                    Id= int.Parse(id.ToString() + "1234"),
                    Amount = 200 *id
                },
                new Account
                {
                    Id=int.Parse(id.ToString() + "2345"),
                    Amount = 300 *id
                },
                new Account
                {
                    Id=int.Parse(id.ToString() + "3456"),
                    Amount = 400 *id
                }
            };
        }
    }
}

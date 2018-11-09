using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JensBankWebApp.Models
{
    public class BankRepository : IBankRepository
    {
        private List<Customer> _customers;

        public BankRepository()
        {
            PopulateCustomersAndAccounts();
        }

        public List<Customer> GetAllCustomers()
        {
            return _customers;
        }

        public Customer GetCustomerById(int id)
        {
            return _customers.SingleOrDefault(c => c.Id == id);
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

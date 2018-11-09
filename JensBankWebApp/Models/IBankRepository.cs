using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JensBankWebApp.Models
{
    public interface IBankRepository
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
    }
}

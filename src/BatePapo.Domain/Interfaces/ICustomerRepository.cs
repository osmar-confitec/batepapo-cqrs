using BatePapo.Domain.Models;
using BatePapo.DomainCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.Domain.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>
    {
   
        Task<bool> CustomerCPFExist(Customer customer);

        Task<bool> CustomerCPFExistNewCustomer(Customer customer);

    }
}

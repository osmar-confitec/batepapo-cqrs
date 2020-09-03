using BatePapo.Data.Context;
using BatePapo.Domain.Interfaces;
using BatePapo.Domain.Models;
using BatePapo.DomainCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {



        public CustomerRepository(BatePapoContext context)
            : base(context)
        {

        }



        public async Task<bool> CustomerCPFExist(Customer customer)
        {

            var c = customer.CPF.GetDocumentNotFormatted();
            return await DbSet.AsNoTracking().AnyAsync(x => 
                                x.Id.ToString() != customer.Id.ToString()
                            && x.CPF.Doc == c);
        }

        public async Task<bool> CustomerCPFExistNewCustomer(Customer customer)
        {
                var c = customer.CPF.GetDocumentNotFormatted();
                return await DbSet.AsNoTracking().AnyAsync(x =>
                          x.CPF.Doc == c);
           
            
        }
    }
}

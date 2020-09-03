using BatePapo.Data.Context;
using BatePapo.DomainCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BatePapoContext _context;

        public UnitOfWork(BatePapoContext context)
        {
            _context = context;
        }

        public bool Commit()
        {
            try
            {
                return  _context.SaveChanges() > 0;
            }
            catch (Exception e)
            {

                throw;
            }
           
        }

        public void Dispose()
        {
            
        }
    }
}

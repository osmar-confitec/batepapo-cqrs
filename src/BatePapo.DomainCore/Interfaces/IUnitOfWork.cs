using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.DomainCore.Interfaces
{
   public interface IUnitOfWork : IDisposable
    {
       bool Commit();
    }
}

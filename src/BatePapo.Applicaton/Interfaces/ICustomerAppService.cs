using BatePapo.Applicaton.EventSourcedNormalizers;
using BatePapo.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BatePapo.Applicaton.Interfaces
{
    public interface ICustomerAppService : IDisposable
    {
        Task Register(CustomerViewModel customerViewModel);
        IEnumerable<CustomerViewModel> GetAll();
        CustomerViewModel GetById(Guid id);
        Task Update(CustomerViewModel customerViewModel);
        Task RemoveAsync(Guid id);
        IList<CustomerHistoryData> GetAllHistory(Guid id);
    }
}

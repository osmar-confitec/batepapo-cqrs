using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BatePapo.Applicaton.EventSourcedNormalizers;
using BatePapo.Applicaton.Interfaces;
using BatePapo.Data.EventSourcing;
using BatePapo.Domain.Commands;
using BatePapo.Domain.Interfaces;
using BatePapo.DomainCore.Bus;
using BatePapo.ViewModel;

namespace BatePapo.Applicaton.Services
{
    public class CustomerAppService : ICustomerAppService
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMediatorHandler Bus;

        public CustomerAppService(IMapper mapper,
                                  ICustomerRepository customerRepository,
                                  IMediatorHandler bus,
                                  IEventStoreRepository eventStoreRepository)
        {
            _mapper = mapper;
            _customerRepository = customerRepository;
            Bus = bus;
            _eventStoreRepository = eventStoreRepository;
        }

        public IEnumerable<CustomerViewModel> GetAll()
        {
            return _customerRepository.GetAll().ProjectTo<CustomerViewModel>(_mapper.ConfigurationProvider);
        }

        public CustomerViewModel GetById(Guid id)
        {
            return _mapper.Map<CustomerViewModel>(_customerRepository.GetById(id));
        }

        public async Task Register(CustomerViewModel customerViewModel)
        {
            var registerCommand = _mapper.Map<RegisterNewCustomerCommand>(customerViewModel);
            await Bus.SendCommand(registerCommand);
        }

        public async Task Update(CustomerViewModel customerViewModel)
        {
            var updateCommand = _mapper.Map<UpdateCustomerCommand>(customerViewModel);
            await Bus.SendCommand(updateCommand);
        }

        public async Task RemoveAsync(Guid id)
        {
            var removeCommand = new RemoveCustomerCommand(id);
            await Bus.SendCommand(removeCommand);
        }

        public IList<CustomerHistoryData> GetAllHistory(Guid id)
        {
            return CustomerHistory.ToJavaScriptCustomerHistory(_eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}

using BatePapo.Domain.Commands;
using BatePapo.Domain.Events;
using BatePapo.Domain.Interfaces;
using BatePapo.Domain.Models;
using BatePapo.DomainCore.Bus;
using BatePapo.DomainCore.Interfaces;
using BatePapo.DomainCore.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatePapo.Domain.CommandHandlers
{
   public  class CustomerCommandHandler : CommandHandler,
        IRequestHandler<RegisterNewCustomerCommand, bool>,
        IRequestHandler<UpdateCustomerCommand, bool>,
        IRequestHandler<RemoveCustomerCommand, bool>
    {

        private readonly ICustomerRepository _customerRepository;
        private readonly IMediatorHandler Bus;

        public CustomerCommandHandler(ICustomerRepository customerRepository,
                                 IUnitOfWork uow,
                                 IMediatorHandler bus,
                                 INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
            _customerRepository = customerRepository;
            Bus = bus;
        }

        public async Task<bool> Handle(RemoveCustomerCommand request, CancellationToken cancellationToken)
        {
            if(!await NotifyValidationErrorsAsync(request))
                return false;

            _customerRepository.Remove(request.Id);

            if (await Commit())
            {
               await Bus.RaiseEvent(new CustomerRemovedEvent(request.Id));
            }
            return true;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!await NotifyValidationErrorsAsync(request))
                return false;


            var updateCustomer = new Customer(request.Id, new DomainCore.ValueObjects.CPF(request.CPF), request.Name, request.SurName, request.NickName, request.BirthDate);

            if (!await NotifyValidationErrorsAsync(updateCustomer))
                return false;

            if (await _customerRepository.CustomerCPFExist(updateCustomer))
            {
                 await  Bus.RaiseEvent(new DomainNotification("CPF_Existente", "Já existe CPF cadastrado para o cliente."));
                return false;
            }

            _customerRepository.Update(updateCustomer);

            if (await Commit())
            {
                await Bus.RaiseEvent(new CustomerUpdatedEvent(updateCustomer.Id,  updateCustomer.Name, updateCustomer.SurName, updateCustomer.NickName, updateCustomer.BirthDate, updateCustomer.CPF.ToString()));
            }
            return true;
        }

        public async Task<bool> Handle(RegisterNewCustomerCommand request, CancellationToken cancellationToken)
        {
            if (!await NotifyValidationErrorsAsync(request))
                return false;


            var newCustomer = new Customer(Guid.NewGuid(), new DomainCore.ValueObjects.CPF(request.CPF), request.Name, request.SurName, request.NickName,  request.BirthDate);

            if (!await NotifyValidationErrorsAsync(newCustomer))
                return false;

            if (await _customerRepository.CustomerCPFExistNewCustomer(newCustomer))
            {
                await Bus.RaiseEvent(new DomainNotification("CPF_Existente", "Já existe CPF cadastrado para o cliente."));
                return false;
            }

            _customerRepository.Add(newCustomer);

            if (await Commit())
            {
                await Bus.RaiseEvent(new CustomerRegisteredEvent(newCustomer.Id, newCustomer.Name, newCustomer.SurName, newCustomer.NickName,  newCustomer.BirthDate, newCustomer.CPF.ToString()));
            }
            return true;
        }
    }
}

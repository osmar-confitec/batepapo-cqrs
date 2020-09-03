using BatePapo.Applicaton.Interfaces;
using BatePapo.Applicaton.Services;
using BatePapo.Bus;
using BatePapo.Data.Context;
using BatePapo.Data.EventSourcing;
using BatePapo.Data.Repository;
using BatePapo.Data.UoW;
using BatePapo.Domain.CommandHandlers;
using BatePapo.Domain.Commands;
using BatePapo.Domain.EventHandlers;
using BatePapo.Domain.Events;
using BatePapo.Domain.Interfaces;
using BatePapo.DomainCore.Bus;
using BatePapo.DomainCore.Events;
using BatePapo.DomainCore.Interfaces;
using BatePapo.DomainCore.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatePapo.Ioc
{
   public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();
          
            // Application
            services.AddScoped<ICustomerAppService, CustomerAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<CustomerRegisteredEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerUpdatedEvent>, CustomerEventHandler>();
            services.AddScoped<INotificationHandler<CustomerRemovedEvent>, CustomerEventHandler>();

            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterNewCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateCustomerCommand, bool>, CustomerCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveCustomerCommand, bool>, CustomerCommandHandler>();

            // Infra - Data
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<BatePapoContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSqlRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();
            services.AddScoped<EventStoreSqlContext>();


        }
    }
}

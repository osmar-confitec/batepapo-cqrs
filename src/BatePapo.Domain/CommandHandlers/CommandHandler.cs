using BatePapo.DomainCore.Bus;
using BatePapo.DomainCore.Commands;
using BatePapo.DomainCore.DomainObjects;
using BatePapo.DomainCore.Interfaces;
using BatePapo.DomainCore.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.Domain.CommandHandlers
{
   public abstract class CommandHandler
    {

        private readonly IUnitOfWork _uow;
        private readonly IMediatorHandler _bus;
        private readonly DomainNotificationHandler _notifications;

        public CommandHandler(IUnitOfWork uow, IMediatorHandler bus, INotificationHandler<DomainNotification> notifications)
        {
            _uow = uow;
            _notifications = (DomainNotificationHandler)notifications;
            _bus = bus;
        }


        protected async Task<bool> NotifyValidationErrorsAsync(Command message)
        {
            if (!await message.EhValido())
            {
                foreach (var error in message.NotificacoesErros)
                {
                   await _bus.RaiseEvent(new DomainNotification(message.MessageType, error.ToString(), true));
                }
                return false;
            }
            return true;
        }

        protected async Task<bool> NotifyValidationErrorsAsync(Entity message)
        {
            if (!await message.EhValido())
            {
                foreach (var error in message.NotificacoesErros)
                {
                    await _bus.RaiseEvent(new DomainNotification(message.GetType().Name, error.ToString(), true));
                }
                return false;
            }
            return true;
        }

        public async Task<bool> Commit()
        {
            if (_notifications.HasNotifications()) return false;
            if ( _uow.Commit()) return true;

            await _bus.RaiseEvent(new DomainNotification("Commit", "Houve problemas Para Salvar os Dados"));
            return false;
        }



    }
}

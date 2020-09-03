using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatePapo.CrossCutting;
using BatePapo.DomainCore.Bus;
using BatePapo.DomainCore.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BatePapo.Services.Api.Controllers
{
    public abstract class ApiController : Controller
    {

        private readonly DomainNotificationHandler _notifications;
        private readonly IMediatorHandler _mediator;


        protected ApiController(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediator)
        {
            _notifications = (DomainNotificationHandler)notifications;
            _mediator = mediator;
        }

        protected IEnumerable<DomainNotification> Notifications => _notifications.GetNotifications();

        protected bool IsValidOperation()
        {
            return (!_notifications.HasNotifications());
        }

        protected IEnumerable<Noty> GetNotys()
        {
            List<Noty> notificacoes = new List<Noty>();

            if (_notifications.GetNotifications().Where(x => x.Noty).Any())
                notificacoes.AddRange(_notifications.GetNotifications().Where(x => x.Noty).Select(n => JsonConvert.DeserializeObject<Noty>(n.Value)));
            
            if (_notifications.GetNotifications().Where(x => !x.Noty).Any())
                notificacoes.AddRange(_notifications.GetNotifications().Where(x=>!x.Noty).Select(n => new Noty { Message = n.Value }));

            return notificacoes;
        }

        protected new IActionResult Response(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = GetNotys()
            });
        }

        protected void NotifyModelStateErrors()
        {
            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotifyError(string.Empty, erroMsg);
            }
        }

        protected void NotifyError(string code, string message)
        {
            _mediator.RaiseEvent(new DomainNotification(code, message));
        }
    }
}

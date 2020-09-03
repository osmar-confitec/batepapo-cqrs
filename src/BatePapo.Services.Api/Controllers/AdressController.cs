using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatePapo.DomainCore.Bus;
using BatePapo.DomainCore.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BatePapo.Services.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AdressController : ApiController
    {
        public AdressController(INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediator) 
                                : base(notifications, mediator)
        {
        }
    }
}

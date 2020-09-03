using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BatePapo.Applicaton.Interfaces;
using BatePapo.DomainCore.Bus;
using BatePapo.DomainCore.Notifications;
using BatePapo.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BatePapo.Services.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiController
    {

        private readonly ICustomerAppService _customerAppService;


        public CustomerController(
                                  ICustomerAppService customerAppService,
                                  INotificationHandler<DomainNotification> notifications,
                                  IMediatorHandler mediator)
            : base(notifications, mediator)
        {

            _customerAppService = customerAppService;

        }

        [HttpGet]
        [Route("customer-management")]
        public IActionResult Get()
        {
            return Response(_customerAppService.GetAll());
        }

        [HttpGet]
        [Route("customer-management/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            var customerViewModel = _customerAppService.GetById(id);

            return Response(customerViewModel);
        }

        [HttpPost]
        [Route("customer-management")]
        public async Task<IActionResult> PostAsync([FromBody] CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(customerViewModel);
            }

            await _customerAppService.Register(customerViewModel);

            return Response(customerViewModel);
        }

        [HttpPut]
        [Route("customer-management")]
        public async Task<IActionResult> PutAsync([FromBody] CustomerViewModel customerViewModel)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(customerViewModel);
            }

            await _customerAppService.Update(customerViewModel);

            return Response(customerViewModel);
        }

        [HttpDelete]
        [Route("customer-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _customerAppService.RemoveAsync(id);
            return Response();
        }

        [HttpGet]
        [Route("customer-management/history/{id:guid}")]
        public IActionResult History(Guid id)
        {
            var customerHistoryData = _customerAppService.GetAllHistory(id);
            return Response(customerHistoryData);
        }
    }
}

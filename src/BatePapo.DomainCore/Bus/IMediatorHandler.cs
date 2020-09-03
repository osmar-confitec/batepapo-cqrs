using BatePapo.DomainCore.Commands;
using BatePapo.DomainCore.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.DomainCore.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}

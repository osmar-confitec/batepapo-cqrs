using BatePapo.DomainCore.Events;
using System;
using System.Collections.Generic;


namespace BatePapo.Data.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        IList<StoredEvent> All(Guid aggregateId);
    }
}
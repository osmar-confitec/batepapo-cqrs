using BatePapo.DomainCore.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BatePapo.Data.EventSourcing
{
    public class SqlEventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;


        public SqlEventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
      
        }

        public void Save<T>(T theEvent) where T : Event
        {
            var serializedData = JsonSerializer.Serialize(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData,
                "");

            _eventStoreRepository.Store(storedEvent);
        }
    }
}

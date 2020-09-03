using BatePapo.DomainCore.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatePapo.DomainCore.Notifications
{
    public class DomainNotification : Event
    {
        public Guid DomainNotificationId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }
        public bool Noty { get; private set; }

        public DomainNotification(string key, string value, bool noty = false)
        {
            DomainNotificationId = Guid.NewGuid();
            Version = 1;
            Key = key;
            Value = value;
        }
    }
}

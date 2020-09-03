using BatePapo.DomainCore.Events;
using System;


namespace BatePapo.Domain.Events
{
    public class CustomerUpdatedEvent : Event
    {
        public CustomerUpdatedEvent(Guid id, string name, string surname, string nickname, DateTime birthDate, string cpf)
        {
            Name = name;
            SurName = surname;
            NickName = nickname;
            BirthDate = birthDate;
            CPF = cpf;
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string SurName { get; private set; }

        public string NickName { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string CPF { get; private set; }
    }
}
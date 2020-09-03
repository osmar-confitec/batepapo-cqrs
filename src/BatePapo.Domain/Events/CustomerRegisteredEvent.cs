using BatePapo.DomainCore.Events;
using System;


namespace BatePapo.Domain.Events
{
    public class CustomerRegisteredEvent : Event
    {
        public CustomerRegisteredEvent(Guid id, string name, string surname, string nickname, DateTime birthDate, string cpf)
        {
            Id = id;
            AggregateId = id;
            Name = name;
            SurName = surname;
            NickName = nickname;
            BirthDate = birthDate;
            CPF = cpf;

        }
        public Guid Id { get; set; }

        public string Name { get; private set; }

        public string SurName { get; private set; }

        public string NickName { get; private set; }

        public DateTime BirthDate { get; private set; }

        public string CPF { get; private set; }
    }
}
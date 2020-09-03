using BatePapo.DomainCore.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BatePapo.Domain.Commands
{
   public abstract class CustomerCommand :Command
    {
        public Guid Id { get; protected set; }

        public string Name { get; protected set; }

        public string SurName { get; protected set; }

        public string NickName { get; protected set; }

        public DateTime BirthDate { get; protected set; }

        public string CPF { get; protected set; }
    }
}

using BatePapo.DomainCore.DomainObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.DomainCore.ValueObjects
{
   public class Email : Entity
    {

        public string Description { get; private set; }

        public async override Task<bool> EhValido() => await ValidEntity(new ValidEmail());



        protected Email():base() { }


        public class ValidEmail : AbstractValidator<Email>
        {

            public ValidEmail()
            {
                RuleFor(x => x.Description).EmailAddress().WithMessage(" Atenção! Email inválido. ");
            }

        }
    }
}

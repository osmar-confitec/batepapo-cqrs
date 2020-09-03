using System;
using BatePapo.CrossCutting;
using BatePapo.Domain.Commands;

using FluentValidation;

namespace BatePapo.Domain.Validations
{
    public abstract class CustomerValidation<T> : AbstractValidator<T> where T : CustomerCommand
    {
        protected void ValidateName()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Por favor entre com seu Nome")
                .Length(2, 150).WithMessage("O nome deve estar entre 2 e 150 caracteres.");
        }

        protected void ValidateCPF()
        {

            RuleFor(c => c.SurName)
            .NotEmpty().WithMessage("Por favor entre com seu CPF")
            .Must(x=>!x.ECpf()).WithMessage("O CPF é inválido");

        }

        protected void ValidateBirthDate()
        {
            RuleFor(c => c.BirthDate)
                .NotEmpty()
                .Must(HaveMinimumAge)
                .WithMessage("The customer must have 18 years or more");
        }

        protected void ValidateSurName()
        {
            RuleFor(c => c.SurName)
                .NotEmpty().WithMessage("Por favor entre com seu Nome")
                .Length(2, 150).WithMessage("O nome deve estar entre 2 e 150 caracteres.");
        }

        protected void ValidateNickName()
        {
            RuleFor(c => c.NickName)
                .NotEmpty().WithMessage("Por favor entre com seu Nome")
                .Length(2, 150).WithMessage("O nome deve estar entre 2 e 150 caracteres.");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Atenção Id deve conter valores.");
        }

        protected static bool HaveMinimumAge(DateTime birthDate)
        {
            return birthDate <= DateTime.Now.AddYears(-18);
        }
    }
}
using BatePapo.Domain.Validations;
using System;
using System.Threading.Tasks;

namespace BatePapo.Domain.Commands
{
    public class UpdateCustomerCommand : CustomerCommand
    {
        public UpdateCustomerCommand(Guid id, string name, string surname, string nickname, DateTime birthDate, string cpf)
        {
            Id = id;
            Name = name;
            SurName = surname;
            NickName = nickname;
            BirthDate = birthDate;
            CPF = cpf;
        }

        public async override Task<bool> EhValido() => await ValidEntity(new UpdateCustomerCommandValidation());
    }
}
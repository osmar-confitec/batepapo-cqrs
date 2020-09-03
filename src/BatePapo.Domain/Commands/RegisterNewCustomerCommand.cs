using BatePapo.Domain.Validations;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.Domain.Commands
{
    public class RegisterNewCustomerCommand : CustomerCommand
    {

        public RegisterNewCustomerCommand(string name, string surfacename, string nickname, DateTime birthDate, string cpf)
        {
            Name = name;
            SurName = surfacename;
            NickName = nickname;
            BirthDate = birthDate;
            CPF = cpf;
        }

        public async override Task<bool> EhValido() => await ValidEntity(new RegisterNewCustomerCommandValidation());
    }
}

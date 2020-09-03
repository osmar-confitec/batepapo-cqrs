using BatePapo.DomainCore.DomainObjects;
using BatePapo.DomainCore.ValueObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatePapo.CrossCutting;
using BatePapo.Domain.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BatePapo.Domain.Models
{

    public enum TypeCliente
    { 
        
        Debit = 10,
        Credit = 11,
        WithoutAnalysis = 12

    }

    public class Customer : Entity, IAggregateRoot
    {

        public  CPF CPF { get; private set; }

        public string Name { get; private set; }

        public string SurName { get; private set; }

        public string NickName { get; private set; }

        public DateTime BirthDate { get; private set; }

        public TypeCliente TypeCliente { get; private set; }


    


        private readonly List<Address> _addresses;
        public IReadOnlyCollection<Address> Addresses => _addresses;


        //Ef Contructor
        protected Customer() : base() {

            _addresses = new List<Address>();
            TypeCliente = TypeCliente.WithoutAnalysis;

        }

        public Customer(Guid id, CPF cPF, string name, string surName, string nickName, DateTime birthDate)
        {
            CPF = cPF;
            Name = name;
            SurName = surName;
            NickName = nickName;
            BirthDate = birthDate;
            Id = id;
        }

        public void AddAddress(Address address)
        {
            if (!_addresses.Any(x => x.Equals(address)))
                _addresses.Add(address);
        }


        public void GerarClienteComCreDito()
        {
            TypeCliente = TypeCliente.Credit;

        }

        public void RemoveAddress(Address endereco)
        {
            /*descrever regras de endereço*/
            if (_addresses.Any(x => x.Id == endereco.Id))
                _addresses.RemoveAll(x => x.Id == endereco.Id);
        }

        public void EditAdress(Address address )
        {
            /*descrever regras de endereço*/
            if (_addresses.Any(x => x.Id == address.Id))
            {
                var endEdit = _addresses.FirstOrDefault(x => x.Id == address.Id);

                if (endEdit != null)
                {
                    RemoveAddress(address);
                    AddAddress(address);
                }
            }
        }



        public async override Task<bool> EhValido()
        {
            var result = await new ValidCliente().ValidateAsync(this);
            var resultCPF = await CPF.EhValido();
            if (!resultCPF)
                AdicionarNotificacaoErro(CPF.NotificacoesErros);
            return result.IsValid && resultCPF;
        }


        public static class CustomerFactory
        {

            public static Customer GenerateCustomerWithCredit(Guid id, CPF cPF, string name, string surName, string nickName, DateTime birthDate)
            {

                var cli = new Customer(id, cPF, name, surName, nickName, birthDate);
                cli.GerarClienteComCreDito();
                return cli;

            }

        }

        
        public class ValidCliente : AbstractValidator<Customer>
        {
            public ValidCliente()
            {
                RuleFor(x => x.NickName).NotEmpty().WithMessage("Por favor entre com seu Nome")
                .Length(2, 150).WithMessage("O nome deve estar entre 2 e 150 caracteres.");

                RuleFor(x => x.Name).NotEmpty().WithMessage("Por favor entre com seu Nome")
                       .Length(2, 150).WithMessage("O nome deve estar entre 2 e 150 caracteres.");
                RuleFor(x => x.SurName).NotEmpty().WithMessage("Por favor entre com seu Nome")
                .Length(2, 150).WithMessage("O nome deve estar entre 2 e 150 caracteres.");


                RuleFor(x => x.BirthDate).NotEmpty()
                    .Must(HaveMinimumAge)
                    .WithMessage("The customer must have 18 years or more");
            }


            public static bool HaveMinimumAge(DateTime birthDate)
            {
                return birthDate <= DateTime.Now.AddYears(-18);
            }



        }


    }
}

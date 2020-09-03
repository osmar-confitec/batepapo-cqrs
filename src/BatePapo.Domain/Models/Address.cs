using BatePapo.DomainCore.DomainObjects;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatePapo.Domain.Models
{

    public enum AddressType
    {

        Delivery = 1,
        Residential = 2,
        Company = 3,
        Thirst = 4,
        Branch = 5

    }

    public class Address : Entity
    {

        public string Neighborhood { get; private set; }

        public string Number { get; private set; }

        public string PublicPlace { get; private set; }

        public AddressType AddressType { get; private set; }

        public bool Principal { get; private set; }


        public Guid CustomerId { get; private set; }


        internal void AssociarCustomer(Guid guid )
        {
            CustomerId = guid;
        }

        // EF Rel.
        public Customer Customer { get; set; }

        public override bool Equals(object obj)
        {
            var compareTo = obj as Address;

            if (compareTo.PublicPlace.ToLower().Equals(PublicPlace.ToLower())
             && compareTo.Number.ToLower().Equals(Number)
             && compareTo.Neighborhood.ToLower().Equals(Neighborhood.ToLower()))
                return true;

            return base.Equals(obj);
        }


        public override int GetHashCode()=> base.GetHashCode();

        // Empty constructor for EF
        protected Address() : base()
        {
            AddressType = AddressType.Residential;
        }

        public Address(string neighborhood, string number, string publicPlace, AddressType addressType, bool principal)
        {
            Neighborhood = neighborhood;
            Number = number;
            PublicPlace = publicPlace;
            AddressType = addressType;
            Principal = principal;
        }



        public override async Task<bool> EhValido() => await ValidEntity(new AddressValid());



        public class AddressValid : AbstractValidator<Address>
        {

            public AddressValid()
            {
                RuleFor(x => x.Neighborhood).NotEmpty().NotEmpty().WithMessage("Por favor entre com seu Nome")
                .Length(2, 150).WithMessage("O Bairro deve estar entre 2 e 150 caracteres.");


                RuleFor(x => x.Number).NotEmpty().WithMessage("Por favor entre com seu Numero")
                .Length(3, 30).WithMessage("O Número deve estar entre 3 e 30 caracteres.");


                RuleFor(x => x.PublicPlace).NotEmpty().WithMessage("Por favor entre com seu Logradouro")
                .Length(10, 150).WithMessage("O Logradouro deve estar entre 10 e 150 caracteres.");
            }
        }
    }
}

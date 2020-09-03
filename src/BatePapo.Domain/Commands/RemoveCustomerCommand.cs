using BatePapo.Domain.Validations;
using System;
using System.Threading.Tasks;

namespace BatePapo.Domain.Commands
{
    public class RemoveCustomerCommand : CustomerCommand
    {
        public RemoveCustomerCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public async override Task<bool> EhValido() => await ValidEntity(new RemoveCustomerCommandValidation());
    }
}
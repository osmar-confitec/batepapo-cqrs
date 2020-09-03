
using BatePapo.Domain.Commands;

namespace BatePapo.Domain.Validations
{
    public class RemoveCustomerCommandValidation : CustomerValidation<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidation()
        {
            ValidateId();
        }
    }
}
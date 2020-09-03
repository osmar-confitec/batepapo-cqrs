

using BatePapo.Domain.Commands;

namespace BatePapo.Domain.Validations
{
    public class UpdateCustomerCommandValidation : CustomerValidation<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidation()
        {
            ValidateId();
            ValidateName();
            ValidateBirthDate();
            ValidateNickName();
            ValidateSurName();
            ValidateCPF();
        }
    }
}

using BatePapo.Domain.Commands;

namespace BatePapo.Domain.Validations
{
    public class RegisterNewCustomerCommandValidation : CustomerValidation<RegisterNewCustomerCommand>
    {
        public RegisterNewCustomerCommandValidation()
        {
            ValidateName();
            ValidateBirthDate();
            ValidateSurName();
            ValidateNickName();
            ValidateCPF();
        }
    }
}
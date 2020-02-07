using AgroBarn.Domain.ApiModels.V1.Request;
using FluentValidation;

namespace AgroBarn.Domain.Validators.V1
{
    public class UserLoginValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Password).NotEmpty();
        }
    }
}

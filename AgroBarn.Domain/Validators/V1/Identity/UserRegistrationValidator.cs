using AgroBarn.Domain.ApiModels.V1.Request;
using FluentValidation;

namespace AgroBarn.Domain.Validators.V1
{
   public  class UserRegistrationValidator : AbstractValidator<UserRegistrationRequest>
    {
        public UserRegistrationValidator()
        {
            RuleFor(c => c.Email).EmailAddress();
            RuleFor(c => c.Password).NotEmpty();
            RuleFor(c => c.ConfirmPassword).NotEmpty();
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.FirstSurname).NotEmpty();
            RuleFor(c => c.PhoneNumber).NotEmpty().Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}");

            RuleFor(c => c).Custom((c, context) =>
            {
                if (c.Password.Trim() != "" && c.ConfirmPassword.Trim() != "" && c.Password != c.ConfirmPassword)
                {
                    context.AddFailure(nameof(c.Password), "La contraseña no coincide");
                }
            });
        }
    }
}

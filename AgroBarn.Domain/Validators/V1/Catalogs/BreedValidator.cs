using AgroBarn.Domain.ApiModels.V1.Request;
using FluentValidation;

namespace AgroBarn.Domain.Validators.V1
{
    public class BreedValidator : AbstractValidator<BreedRequest>
    {
        public BreedValidator()
        {
            RuleFor(v => v.Name).NotEmpty().Length(5, 100);
        }
    }
}

using FluentValidation;

namespace CryptoTestApi.Application.Validations;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(x => x).NotEmpty();
    }
}
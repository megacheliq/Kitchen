using FluentValidation;
using Kitchen.Service.Domain.Kitchen.UseCases.Commands;

namespace Kitchen.Service.Domain.Kitchen.Validators
{
    public class CreateKitchenCommandValidator : AbstractValidator<CreateKitchenCommand>
    {
        public CreateKitchenCommandValidator()
        {
            RuleFor(command => command.CommandDto)
                .NotNull()
                .SetValidator(new AddOrUpdateKitchenDtoValidator());
        }
    }
}

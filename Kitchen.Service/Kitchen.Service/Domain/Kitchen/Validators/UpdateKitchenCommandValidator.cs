using FluentValidation;
using Kitchen.Service.Domain.Kitchen.UseCases.Commands;

namespace Kitchen.Service.Domain.Kitchen.Validators
{
    public class UpdateKitchenCommandValidator : AbstractValidator<UpdateKitchenCommand>
    {
        public UpdateKitchenCommandValidator()
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("Идентификатор не должен быть пустым");

            RuleFor(command => command.Dto)
                .NotNull()
                .SetValidator(new AddOrUpdateKitchenDtoValidator());
        }
    }
}

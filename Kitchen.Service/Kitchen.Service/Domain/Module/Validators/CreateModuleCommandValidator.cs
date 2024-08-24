using FluentValidation;
using Kitchen.Service.Domain.Module.UseCases.Commands;

namespace Kitchen.Service.Domain.Module.Validators
{
    public class CreateModuleCommandValidator : AbstractValidator<CreateModuleCommand>
    {
        public CreateModuleCommandValidator() 
        {
            RuleFor(command => command.CommandDto)
                .NotNull()
                .SetValidator(new AddOrUpdateModuleDtoValidator());
        }
    }
}

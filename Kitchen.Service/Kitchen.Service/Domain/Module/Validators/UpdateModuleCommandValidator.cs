using FluentValidation;
using Kitchen.Service.Domain.Module.UseCases.Commands;

namespace Kitchen.Service.Domain.Module.Validators
{
    public class UpdateModuleCommandValidator : AbstractValidator<UpdateModuleCommand>
    {
        public UpdateModuleCommandValidator() 
        {
            RuleFor(command => command.Id)
                .NotEmpty()
                .WithMessage("Идентификатор не должен быть пустым");

            RuleFor(command => command.Dto)
                .NotNull()
                .SetValidator(new AddOrUpdateModuleDtoValidator());
        }
    }
}

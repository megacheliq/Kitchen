using FluentValidation;
using Kitchen.Service.Domain.Module.Models;

namespace Kitchen.Service.Domain.Module.Validators
{
    public class AddOrUpdateModuleDtoValidator : AbstractValidator<AddOrUpdateModuleDto>
    {
        public AddOrUpdateModuleDtoValidator() 
        {
            RuleFor(module => module.Name)
                .NotEmpty()
                .WithMessage("Нужно указать название модуля");

            RuleFor(module => module.Width)
                .GreaterThan(0)
                .WithMessage("Нужно указать ширину модуля");

            RuleFor(module => module.Height)
                .GreaterThan(0)
                .WithMessage("Нужно указать высоту модуля");
        }
    }
}

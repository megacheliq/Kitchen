using FluentValidation;
using Kitchen.Service.Domain.Kitchen.Models;

namespace Kitchen.Service.Domain.Kitchen.Validators
{
    public class AddOrUpdateKitchenDtoValidator : AbstractValidator<AddOrUpdateKitchenDto>
    {
        public AddOrUpdateKitchenDtoValidator()
        {
            RuleFor(kitchen => kitchen.Width)
                .GreaterThan(0)
                .WithMessage("Нужно указать ширину кухни");

            RuleFor(kitchen => kitchen.Height)
                .GreaterThan(0)
                .WithMessage("Нужно указать высоту кухни");

            RuleFor(kitchen => kitchen.WaterPipe)
                .NotNull()
                .SetValidator(new CoordinateValidator())
                .DependentRules(() =>
                {
                    RuleFor(kitchen => kitchen)
                        .Must(kitchen =>
                            kitchen.WaterPipe.X >= 0 && kitchen.WaterPipe.X <= kitchen.Width &&
                            kitchen.WaterPipe.Y >= 0 && kitchen.WaterPipe.Y <= kitchen.Height)
                        .WithMessage("Координаты водопровода должны находиться внутри кухни");
                });
        }
    }
}

using FluentValidation;
using Kitchen.Service.DataAccess.Models;

namespace Kitchen.Service.Domain.Kitchen.Validators
{
    public class CoordinateValidator : AbstractValidator<Coordinate>
    {
        public CoordinateValidator() 
        {
            RuleFor(c => c.X)
                .GreaterThan(0)
                .WithMessage("Необходимо задать X объекту");

            RuleFor(c => c.Y)
                .GreaterThan(0)
                .WithMessage("Необходимо задать Y объекту");
        }
    }
}

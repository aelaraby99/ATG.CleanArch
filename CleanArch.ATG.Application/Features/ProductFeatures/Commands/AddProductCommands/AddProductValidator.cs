using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.ATG.Application.Features.ProductFeatures.Commands.AddProductCommands
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductValidator()
        {
            RuleFor(p=>p.name)
                .NotEmpty()
                .WithMessage("Name is required.")
                .NotNull()
                .WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.price)
                .GreaterThan(10)
                .WithMessage("Price must be greater than 10.")
                .LessThanOrEqualTo(500)
                .WithMessage("Price must be less than 500.")
                .NotNull() ;

        }
    }
}

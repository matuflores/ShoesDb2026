using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Validators
{
    public class BrandValidator:AbstractValidator<Brand>
    {
        public BrandValidator()
        {
            RuleFor(b => b.BrandName).NotEmpty().WithMessage("Brand name is required.")
                .MaximumLength(50).WithMessage("Brand name cannot exceed 50 characters.");

            RuleFor(b => b.Active).NotNull().WithMessage("Activity status must be defined.");

        }
    }
}

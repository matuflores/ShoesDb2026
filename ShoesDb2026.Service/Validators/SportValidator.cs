using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Validators
{
    public class SportValidator:AbstractValidator<Sport>
    {
        public SportValidator() 
        { 
            RuleFor(sp => sp.SportName).NotEmpty().WithMessage("Sport name is required.")
                .Must(sp => !string.IsNullOrWhiteSpace(sp)).WithMessage("Sport name cannot be empty or whitespace.")
                .MaximumLength(100).WithMessage("Sport name cannot exceed 100 characters.");

            RuleFor(sp => sp.Active).NotNull().WithMessage("Activity status must be defined.");
        }
    }
}

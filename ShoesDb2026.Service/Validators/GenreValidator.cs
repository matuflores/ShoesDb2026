using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Validators
{
    public class GenreValidator:AbstractValidator<Genre>
    {
        public GenreValidator()
        {
            RuleFor(g => g.GenreName).NotEmpty().WithMessage("Genre name is required.")
                .MaximumLength(10).WithMessage("Genre name cannot exceed 10 characters.");

            RuleFor(g => g.Active).NotNull().WithMessage("Activity status must be defined.");
        }
    }
}

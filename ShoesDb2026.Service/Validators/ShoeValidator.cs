using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Validators
{
    public class ShoeValidator:AbstractValidator<Shoe>
    {
        public ShoeValidator() 
        { 
            RuleFor(s => s.Model).NotEmpty().WithMessage("Model is required.")
                .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage("Title cannot be empty or whitespace.")
                .MaximumLength(150).WithMessage("Model cannot exceed 150 characters.");

            RuleFor(s => s.Description).NotEmpty().WithMessage("Description is required.")
                .Must(s => !string.IsNullOrWhiteSpace(s)).WithMessage("Title cannot be empty or whitespace.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(s => s.Price).GreaterThan(0).WithMessage("Price must be greater than zero.")
                .LessThan(1000000).WithMessage("Price is unrealistically high.");

            RuleFor(s => s.Active).NotNull().WithMessage("Activity status must be defined.");

            RuleFor(s => s.BrandId).GreaterThan(0).WithMessage("Brand ID must be a positive integer.");

            RuleFor(s => s.SportId).GreaterThan(0).WithMessage("Sport ID must be a positive integer.");

            RuleFor(s => s.GenreId).GreaterThan(0).WithMessage("Genre ID must be a positive integer.");

        //    public int ShoeId { get; set; }
        //public string Model { get; set; } = null!;
        //public string Description { get; set; } = null!;
        //public decimal Price { get; set; }
        //public bool Active { get; set; }

        //public int BrandId { get; set; }
        //public Brand Brand { get; set; } = null!;
        //public int SportId { get; set; }
        //public Sport Sport { get; set; } = null!;
        //public int GenreId { get; set; }
        //public Genre Genre { get; set; } = null!;
    }
    }
}

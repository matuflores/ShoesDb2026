using FluentValidation;
using ShoesDb2026.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShoesDb2026.Service.Validators
{
    public class SizeValidator:AbstractValidator<Size>
    {
        public SizeValidator() 
        {
            RuleFor(sz => sz.SizeNumber)
                .GreaterThan(0).WithMessage("Size value must be greater than zero.")
                .LessThan(55).WithMessage("Size value is unrealistically high.");

            RuleFor(sz => sz.Active).NotNull().WithMessage("Activity status must be defined.");//aca lo que hago es
    }
    }
}

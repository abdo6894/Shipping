using AppResources;
using BL.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Validators
{
    public class ShippingTypeDtoValidator : AbstractValidator<TbShippingTypeDto>
    {
        public ShippingTypeDtoValidator()
        {
            RuleFor(x => x.ShippingTypeAname)
       .NotEmpty().WithMessage(Messages.NameArRequired).
        Length(5, 100).WithMessage(Messages.NameLenght);

                       RuleFor(x => x.ShippingTypeEname).NotEmpty().WithMessage(Messages.NameEnRequired).
                Length(5, 100).WithMessage(Messages.NameLenght);


            RuleFor(x => x.ShippingFactor)
            .NotEmpty()
            .WithMessage(Shipping.FactorRequired)
            .InclusiveBetween(0.25, 10)
            .WithMessage(Shipping.FactorRange);
        }


    }
}
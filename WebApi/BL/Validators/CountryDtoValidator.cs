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
    public class CountryDtoValidator : AbstractValidator<TbCountryDto>
    {
        public CountryDtoValidator()
        {
            RuleFor(x => x.CountryAname)
                .NotEmpty()
                .WithMessage(Messages.NameArRequired)
                .Length(3, 100)
                .WithMessage(Messages.NameLenght);

            RuleFor(x => x.CountryEname)
                .NotEmpty()
                .WithMessage(Messages.NameArRequired)
                .Length(3, 100)
                .WithMessage(Messages.NameLenght);
        }
    }
}

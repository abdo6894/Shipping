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
    public class CitiesDtoValidator : AbstractValidator<TbCityDto>
    {
        public CitiesDtoValidator()
        {
            RuleFor(x => x.CityAname)
       .NotEmpty().WithMessage(Messages.NameArRequired).
        Length(3, 100).WithMessage(Messages.NameLenght);

                       RuleFor(x => x.CityEname).NotEmpty().WithMessage(Messages.NameEnRequired).
                Length(3, 100).WithMessage(Messages.NameLenght);



            RuleFor(x => x.CountryId).NotNull().WithMessage(Messages.CountryRequired);
        }


    }
}
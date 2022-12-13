using FluentValidation;
using IKnowTechnology.BLL.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.BLL.Validators
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("Lütfen isminizi giriniz.");
            RuleFor(a => a.LastName).NotEmpty().WithMessage("Lütfen soyadınızı giriniz.");            
        }
    }
}

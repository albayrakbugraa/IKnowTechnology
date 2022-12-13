using FluentValidation;
using IKnowTechnology.BLL.Models.DTOs;
using IKnowTechnology.BLL.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKnowTechnology.BLL.Validators
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(a => a.FirstName).NotEmpty().WithMessage("Lütfen isminizi giriniz.");
            RuleFor(a => a.LastName).NotEmpty().WithMessage("Lütfen soyadınızı giriniz.");
            RuleFor(a => a.Email).NotEmpty().WithMessage("Lütfen mail adresinizi giriniz.").EmailAddress().WithMessage("Email formatı hatalı !");
            RuleFor(a => a.Password).NotEmpty().WithMessage("Lütfen şifre giriniz.");
            RuleFor(a => a.ConfirmPassword).NotEmpty().WithMessage("Lütfen şifre tekrarını giriniz.");
        }
    }
}

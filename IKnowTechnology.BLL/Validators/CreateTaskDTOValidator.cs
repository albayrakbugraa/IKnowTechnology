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
    public class CreateTaskDTOValidator : AbstractValidator<CreateTaskDTO>
    {
       public CreateTaskDTOValidator()
       {
            RuleFor(a => a.Title).NotEmpty().WithMessage("Lütfen başlığı boş bırakmayınız.");
            RuleFor(a => a.WorkTime).NotEmpty().WithMessage("Lütfen tarih seçiniz.");
       }
    }
}

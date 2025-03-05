using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Validators
{
    public class AddProjectRequestValidator : AbstractValidator<AddProjectRequest>
    {
        public AddProjectRequestValidator()
        {
            RuleFor(x => x.NameEN)
                .NotEmpty()
                .WithMessage("NameEN is required.");
            RuleFor(x => x.NameAR)
                .NotEmpty()
                .WithMessage("NameAR is required.");
            RuleFor(x => x.DescriptionEN)
                .NotEmpty()
                .WithMessage("DescriptionEN is required.");
            RuleFor(x => x.DescriptionAR)
                .NotEmpty()
                .WithMessage("DescriptionAR is required.");
            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required.");
            RuleFor(x => x.Type)
                .NotEmpty()
                .WithMessage("Type is required.");
            RuleFor(x => x.HeadImage)
                .NotNull()
                .WithMessage("HeadImage is required.");
           
        }
    }
}

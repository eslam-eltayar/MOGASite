using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Validators
{
    public class AddServiceRequestValidator : AbstractValidator<AddServiceRequest>
    {
        public AddServiceRequestValidator()
        {
            RuleFor(x => x.TitleAR)
                .NotEmpty()
                .WithMessage("TitleAR is required.");
            RuleFor(x => x.TitleEN)
                .NotEmpty()
                .WithMessage("TitleEN is required.");
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

        }
    }
}

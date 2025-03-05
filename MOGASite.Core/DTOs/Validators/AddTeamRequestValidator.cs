using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Validators
{
    public class AddTeamRequestValidator : AbstractValidator<AddTeamRequest>
    {
        public AddTeamRequestValidator()
        {
            RuleFor(x => x.FirstNameEN)
                .NotEmpty()
                .WithMessage("FirstNameEN is required.");
            RuleFor(x => x.FirstNameAR)
                .NotEmpty()
                .WithMessage("FirstNameAR is required.");
            RuleFor(x => x.LastNameEN)
                .NotEmpty()
                .WithMessage("LastNameEN is required.");
            RuleFor(x => x.LastNameAR)
                .NotEmpty()
                .WithMessage("LastNameAR is required.");
            
        }
    }
}

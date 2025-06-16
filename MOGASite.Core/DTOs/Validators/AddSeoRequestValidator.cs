using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Validators;
public class AddSeoRequestValidator : AbstractValidator<AddSeoRequest>
{
    public AddSeoRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(550);

        RuleFor(x => x.Keywords)
            .NotEmpty();

        RuleFor(x => x.Route)
            .NotEmpty();

        RuleFor(x => x.OgTitle)
            .NotEmpty();

        RuleFor(x => x.OgDescription)
            .NotEmpty();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGASite.Core.DTOs.Validators
{
    public class AddQuotationRequestValidator : AbstractValidator<AddQuotationRequest>
    {
        public AddQuotationRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName is required.");
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName is required.");
            RuleFor(x => x.BusinessEmail)
                .NotEmpty()
                .WithMessage("BusinessEmail is required.");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required.");
            RuleFor(x => x.CompanyName)
                .NotEmpty()
                .WithMessage("CompanyName is required.");
            RuleFor(x => x.NumberOfEmployees)
                .NotEmpty()
                .WithMessage("NumberOfEmployees is required.");
            RuleFor(x => x.Service)
                .NotEmpty()
                .WithMessage("Service is required.");
        }
    }

}

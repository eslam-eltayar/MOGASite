namespace MOGASite.Core.DTOs.Validators
{
    public class AddContactUsRequestValidator : AbstractValidator<AddContactUsRequest>
    {
        public AddContactUsRequestValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("FullName is required.");
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.");
            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone is required.");
            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message is required.");
            RuleFor(x => x.FindWay)
                .NotEmpty()
                .WithMessage("FindWay is required.");
        }
    }
}

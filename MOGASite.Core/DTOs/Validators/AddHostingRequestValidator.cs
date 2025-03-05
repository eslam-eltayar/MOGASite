namespace MOGASite.Core.DTOs.Validators
{
    public class AddHostingRequestValidator : AbstractValidator<AddHostingRequest>
    {
        public AddHostingRequestValidator()
        {
            RuleFor(x => x.NameEN)
                .NotEmpty()
                .WithMessage("NameEN is required.");
            RuleFor(x => x.NameAR)
                .NotEmpty()
                .WithMessage("NameAR is required.");
            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage("Url is required.");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("Price is required.");
            RuleFor(x => x.HostingProperties)
                .NotEmpty()
                .WithMessage("HostingProperties is required.");
        }
    }
}

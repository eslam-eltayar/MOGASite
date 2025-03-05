namespace MOGASite.Core.DTOs.Validators
{
    public class AddClientRequestValidator : AbstractValidator<AddClientRequest>
    {
        public AddClientRequestValidator()
        {
            RuleFor(x => x.NameAR)
                .NotEmpty()
                .WithMessage("NameAR is required.");
            RuleFor(x => x.NameEN)
                .NotEmpty()
                .WithMessage("NameEN is required.");
            RuleFor(x => x.Logo)
                .NotNull()
                .WithMessage("Logo is required.");
        }
    }
}

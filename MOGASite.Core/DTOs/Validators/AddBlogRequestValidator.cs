namespace MOGASite.Core.DTOs.Validators
{
    public class AddBlogRequestValidator : AbstractValidator<AddBlogRequest>
    {
        public AddBlogRequestValidator()
        {
            RuleFor(x => x.TitleAR)
                .NotEmpty()
                .WithMessage("TitleAR is required.");

            RuleFor(x => x.TitleEN)
                .NotEmpty()
                .WithMessage("TitleEN is required.");

            RuleFor(x => x.DescriptionAR)
                .NotEmpty()
                .WithMessage("DescriptionAR is required.");

            RuleFor(x => x.DescriptionEN)
                .NotEmpty()
                .WithMessage("DescriptionEN is required.");

            RuleFor(x => x.ContentAR)
                .NotEmpty()
                .WithMessage("ContentAR is required.");

            RuleFor(x => x.ContentEN)
                .NotEmpty()
                .WithMessage("ContentEN is required.");

            RuleFor(x => x.Category)
                .NotEmpty()
                .WithMessage("Category is required.");
        }
    }
}

using FluentValidation;

namespace OrderSheet.Application.Commands.Validators
{
    public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.Price)
                .ScalePrecision(10, 2);
        }
    }
}

using FluentValidation;
using Product.DTOs.ProductDto;

namespace Product.Validators.Product
{
    public class ProductPostDtoValidator:AbstractValidator<ProductPostDto>
    {
        public ProductPostDtoValidator()
        {
            RuleFor(x => x.Name).NotNull()
                                .NotEmpty()
                                .MaximumLength(50);
            RuleFor(x => x.Price).NotNull()
                               .NotEmpty()
                               .GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).NotNull()
                                 .NotEmpty()
                                 .GreaterThanOrEqualTo(0);
        }
    }
}

using FluentValidation;
using Product.DTOs.ProductDto;

namespace Product.Validators.Product
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
        }
    }
}

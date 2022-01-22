using FluentValidation;
using Product.DTOs.ProductDto;
using System.Linq;

namespace Product.Validators.Product
{
    public class ProductPatchDtoValidator:AbstractValidator<ProductPatchDto>
    {
        public ProductPatchDtoValidator()
        {
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
        }
    }
}

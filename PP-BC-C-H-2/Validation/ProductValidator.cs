using FluentValidation;
using PP_BC_C_H_2.Entity;

namespace PP_BC_C_H_2.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Unless(x => IsPatchRequest());

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.")
            .Unless(x => IsPatchRequest());
    }

    private bool IsPatchRequest()
    {
        var httpContext = new HttpContextAccessor().HttpContext;
        return httpContext != null && httpContext.Request.Method.Equals("PATCH", StringComparison.OrdinalIgnoreCase);
    }
}
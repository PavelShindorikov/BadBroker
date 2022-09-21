using Application.Contracts;
using FluentValidation;

namespace Application.Common.Validators;
public class BestRevenueRequestValidator : AbstractValidator<BestRevenueRequest>
{
    public BestRevenueRequestValidator()
    {
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Start date is required.")
            .GreaterThanOrEqualTo(new DateTime(1999, 1, 1))
            .WithMessage("Start date must be greater than or equal 1999-01-01");
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("End date is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("EndDate can't be in the future.");
        RuleFor(x => x.Money)
            .NotEmpty().WithMessage("Money is required.");
        RuleFor(x => x)
            .Must(x => x.StartDate < x.EndDate)
            .WithMessage("Start date must be less end date.")
            .Must(x => (x.EndDate - x.StartDate).Days <= 60)
            .WithMessage("The specified period cannot exceed 2 months (60 days).")
            .Must(x => (x.EndDate - x.StartDate).Days >= 2)
            .WithMessage("The specified period cannot be less than 2 days."); ;
    }
}

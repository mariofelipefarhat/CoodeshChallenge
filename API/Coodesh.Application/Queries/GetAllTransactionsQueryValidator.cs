using FluentValidation;

namespace Coodesh.Application.Queries;

public class GetAllTransactionsQueryValidator : AbstractValidator<GetAllTransactionsQuery>
{
    public GetAllTransactionsQueryValidator()
    {
        RuleFor(query => query.Page).GreaterThanOrEqualTo(1);
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1);
        RuleFor(query => query.SortBy).NotNull().NotEmpty().MaximumLength(50);
        RuleFor(query => query.SortDirection).NotNull().NotEmpty().Must(BeAValidSortDirection);
    }

    private bool BeAValidSortDirection(string sortDirection)
    {
        return sortDirection.Equals("asc", StringComparison.OrdinalIgnoreCase) ||
               sortDirection.Equals("desc", StringComparison.OrdinalIgnoreCase);
    }
}
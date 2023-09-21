using FluentValidation;

namespace Assignment.Application.Features.Players.Queries.GetProductsWithPagination
{
    public class GetProductsWithPaginationValidator : AbstractValidator<GetProductsWithPaginationQuery>
    {
        public GetProductsWithPaginationValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}

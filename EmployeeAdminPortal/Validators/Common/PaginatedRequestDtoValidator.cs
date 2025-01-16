using EmployeeAdminPortal.Dtos.Common;
using FluentValidation;

namespace EmployeeAdminPortal.Validators.Common
{
    public class PaginatedRequestDtoValidator : AbstractValidator<PaginatedRequestDto>
    {
        public PaginatedRequestDtoValidator()
        {
            RuleFor(e => e.SearchText)
                .MaximumLength(50).WithMessage("Search text can have at max 50 characters")
                .When(e => !string.IsNullOrEmpty(e.SearchText));

            RuleFor(e => e.SortField)
                .MaximumLength(20).WithMessage("Sort field can have at max 20 characters ")
                .When(e => !string.IsNullOrEmpty(e.SortField));

            RuleFor(e => e.SortOrder)
               .Must(value => value == "asc" || value == "desc")
               .WithMessage("SortOrder must be either 'asc' or 'desc'.");

            RuleFor(e => e.PageNumber)
                .GreaterThan(0).WithMessage("Page Number must be greater than 0");

            RuleFor(e => e.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0");
        }
    }
}

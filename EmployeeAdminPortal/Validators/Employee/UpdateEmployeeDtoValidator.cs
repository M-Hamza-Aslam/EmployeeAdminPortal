using EmployeeAdminPortal.DTOs.Employee;
using FluentValidation;

namespace EmployeeAdminPortal.Validators.Employee
{
    public class UpdateEmployeeDtoValidator : AbstractValidator<AddEmployeeDto>
    {
        public UpdateEmployeeDtoValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(30).WithMessage("Name must not exceed 30 characters.");

            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(e => e.Salary)
                .GreaterThan(0).WithMessage("Salary must be greater than zero.");

            RuleFor(e => e.OfficeId)
                .GreaterThan(0).WithMessage("Invalid office Id.");
        }
    }
}

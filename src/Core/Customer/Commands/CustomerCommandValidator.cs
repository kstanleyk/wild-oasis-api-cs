using FluentValidation;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Commands;

public class CustomerCommandValidator : AbstractValidator<CustomerCommand>
{
    public CustomerCommandValidator()
    {
        RuleFor(p => p.Customer)
            .NotNull()
            .WithMessage("customer can not be empty")
            .SetValidator(new CustomerValidator());
    }
}

public class CustomersCommandValidator : AbstractValidator<CustomersCommand>
{
    public CustomersCommandValidator()
    {
        RuleForEach(x => x.Customers).NotNull().SetValidator(new CustomerValidator());
    }
}

public class CustomerValidator : AbstractValidator<CustomerVm>
{
    public CustomerValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("id is required.")
            .NotNull()
            .WithMessage("id is required.")
            .MaximumLength(10)
            .WithMessage("id must not exceed 10 characters.");

        RuleFor(p => p.FullName)
            .NotEmpty()
            .WithMessage("name is required.")
            .NotNull()
            .WithMessage("name is required.")
            .MaximumLength(200)
            .WithMessage("name must not exceed 200 characters.");

        RuleFor(p => p.Email)
            .NotEmpty()
            .WithMessage("email is required.")
            .NotNull()
            .WithMessage("email is required.")
            .MaximumLength(75)
            .WithMessage("email must not exceed 75 characters.");

        RuleFor(p => p.Nationality)
            .NotEmpty()
            .WithMessage("nationality is required.")
            .NotNull()
            .WithMessage("nationality is required.")
            .MaximumLength(35)
            .WithMessage("nationality must not exceed 35 characters.");

        RuleFor(p => p.NationalId)
            .NotEmpty()
            .WithMessage("national id is required.")
            .NotNull()
            .WithMessage("national id is required.")
            .MaximumLength(75)
            .WithMessage("national id must not exceed 75 characters.");

        RuleFor(p => p.CountryFlag)
            .NotEmpty()
            .WithMessage("country flag is required.")
            .NotNull()
            .WithMessage("country flag is required.")
            .MaximumLength(75)
            .WithMessage("country flag must not exceed 75 characters.");
    }
}

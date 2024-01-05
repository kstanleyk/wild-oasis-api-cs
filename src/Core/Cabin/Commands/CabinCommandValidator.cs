using FluentValidation;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Commands;

public class CabinCommandValidator : AbstractValidator<CabinCommand>
{
    public CabinCommandValidator()
    {
        RuleFor(p => p.Cabin)
            .NotNull()
            .WithMessage("cabin can not be empty")
            .SetValidator(new CabinValidator());
    }
}

public class CabinsCommandValidator : AbstractValidator<CabinsCommand>
{
    public CabinsCommandValidator()
    {
        RuleForEach(x => x.Cabins).NotNull().SetValidator(new CabinValidator());
    }
}

public class CabinValidator : AbstractValidator<CabinVm>
{
    public CabinValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty()
            .WithMessage("id is required.")
            .NotNull()
            .WithMessage("id is required.")
            .MaximumLength(20)
            .WithMessage("id must not exceed 5 characters.");

        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("name is required.")
            .NotNull()
            .WithMessage("name is required.")
            .MaximumLength(20)
            .WithMessage("name must not exceed 20 characters.");

        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("description is required.")
            .NotNull()
            .WithMessage("description is required.")
            .MaximumLength(1000)
            .WithMessage("description must not exceed 1000 characters.");

        RuleFor(p => p.Image)
            .NotEmpty()
            .WithMessage("image url is required.")
            .NotNull()
            .WithMessage("image url is required.")
            .MaximumLength(200)
            .WithMessage("image url must not exceed 200 characters.");
    }
}

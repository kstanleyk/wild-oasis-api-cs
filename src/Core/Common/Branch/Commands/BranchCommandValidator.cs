using FluentValidation;
using WildOasis.Domain.Vm.Common;

namespace WildOasis.Application.Common.Branch.Commands;

public class BranchCommandValidator : AbstractValidator<BranchCommand>
{
    public BranchCommandValidator()
    {
        RuleFor(p => p.Branch)
            .NotNull()
            .WithMessage("branch can not be empty")
            .SetValidator(new BranchValidator());
    }
}

public class BranchesCommandValidator : AbstractValidator<BranchesCommand>
{
    public BranchesCommandValidator()
    {
        RuleForEach(x => x.Branches).NotNull().SetValidator(new BranchValidator());
    }
}

public class BranchValidator : AbstractValidator<BranchVm>
{
    public BranchValidator()
    {
        RuleFor(p => p.Tenant)
            .NotEmpty()
            .WithMessage("Tenant is required.")
            .NotNull()
            .WithMessage("Tenant is required.")
            .MaximumLength(10)
            .WithMessage("Tenant must not exceed 10 characters.");

        RuleFor(p => p.Code)
            .NotEmpty()
            .WithMessage("Code is required.")
            .NotNull()
            .WithMessage("Code is required.")
            .MaximumLength(5)
            .WithMessage("Code must not exceed 5 characters.");


        RuleFor(p => p.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .NotNull()
            .WithMessage("Description is required.")
            .MaximumLength(75)
            .WithMessage("Description must not exceed 75 characters.");

        RuleFor(p => p.BranchName)
            .NotEmpty()
            .WithMessage("Branch Name is required.")
            .NotNull()
            .WithMessage("Branch Name is required.")
            .MaximumLength(75)
            .WithMessage("Branch Name must not exceed 75 characters.");

        RuleFor(p => p.BranchShortName)
            .NotEmpty()
            .WithMessage("Branch Short Name is required.")
            .NotNull()
            .WithMessage("Branch Short Name is required.")
            .MaximumLength(35)
            .WithMessage("Branch Short Name must not exceed 35 characters.");

        RuleFor(p => p.StationCode)
            .NotEmpty()
            .WithMessage("Station Code is required.")
            .NotNull()
            .WithMessage("Station Code is required.")
            .MaximumLength(10)
            .WithMessage("Station Code must not exceed 10 characters.");

        RuleFor(p => p.Address)
            .NotEmpty()
            .WithMessage("Address is required.")
            .NotNull()
            .WithMessage("Address is required.")
            .MaximumLength(50)
            .WithMessage("Address must not exceed 50 characters.");

        RuleFor(p => p.Telephone)
            .NotEmpty()
            .WithMessage("Telephone is required.")
            .NotNull()
            .WithMessage("Telephone is required.")
            .MaximumLength(35)
            .WithMessage("Telephone must not exceed 35 characters.");

        RuleFor(p => p.Region)
            .NotEmpty()
            .WithMessage("Region is required.")
            .NotNull()
            .WithMessage("Region is required.")
            .MaximumLength(2)
            .WithMessage("Region must not exceed 2 characters.");

        RuleFor(p => p.Motto)
            .NotEmpty()
            .WithMessage("Motto is required.")
            .NotNull()
            .WithMessage("Motto is required.")
            .MaximumLength(150)
            .WithMessage("Motto must not exceed 150 characters.");

        RuleFor(p => p.HeadOffice)
            .NotEmpty()
            .WithMessage("Head Office is required.")
            .NotNull()
            .WithMessage("Head Office is required.")
            .MaximumLength(2)
            .WithMessage("Head Office must not exceed 2 characters.");

        RuleFor(p => p.Employer)
           .NotEmpty()
           .WithMessage("Employer is required.")
           .NotNull()
           .WithMessage("Employer is required.")
           .MaximumLength(5)
           .WithMessage("Employer must not exceed 5 characters.");

        RuleFor(p => p.BranchType)
            .NotEmpty()
            .WithMessage("Branch Type is required.")
            .NotNull()
            .WithMessage("Branch Type is required.")
            .MaximumLength(2)
            .WithMessage("Branch Type must not exceed 2 characters.");

        RuleFor(p => p.BranchTown)
            .NotEmpty()
            .WithMessage("Branch Town is required.")
            .NotNull()
            .WithMessage("Branch Town is required.")
            .MaximumLength(35)
            .WithMessage("Branch Town must not exceed 35 characters.");
    }
}

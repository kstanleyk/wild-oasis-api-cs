using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Core;

namespace WildOasis.Application.Customer.Commands;

public abstract class CustomerCommandBase : RequestHandlerBase
{
    private readonly IMapper _mapper;

    protected CustomerCommandBase(IMapper mapper)
    {
        _mapper = mapper;
    }

    protected async
        Task<(CustomerCommandResponse response, Domain.Entity.Customer
            branch)> ValidateCustomerAsync(
        CustomerCommand request,
        CancellationToken cancellationToken)
    {
        var response = new CustomerCommandResponse();

        var validator = new CustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                if (!response.ValidationErrors.Contains(error.ErrorMessage))
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }
        }

        if (!response.Success) return (response, null);

        var entity = _mapper.Map<Domain.Entity.Customer>(request.Customer);

        return (response, entity);
    }


    protected async Task<(CustomersCommandResponse response, Domain.Entity.Customer[] branches)>
        ValidateCustomersAsync(CustomersCommand request, CancellationToken cancellationToken)
    {
        var response = new CustomersCommandResponse();

        if (request.Customers is null)
        {
            response.Success = false;
            response.ValidationErrors.Add("list of customers can not be empty");
            return (response, null);
        }

        var validator = new CustomersCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors.Where(error =>
                         !response.ValidationErrors.Contains(error.ErrorMessage)))
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
        }

        if (!response.Success) return (response, null);

        var branches = _mapper.Map<Domain.Entity.Customer[]>(request.Customers);

        return (response, branches);
    }
    protected static CustomerCommand Sanitize(CustomerCommand command)
    {
        if (command.Customer != null)
            command.Customer = PropertySanitizer.TrimWhiteSpaceOnRequest(command.Customer);

        return command;
    }

    protected static CustomersCommand Sanitize(CustomersCommand command)
    {
        if (command.Customers == null) return command;
        for (var i = 0; i <= command.Customers.Length - 1; i++)
            command.Customers[i] = PropertySanitizer.TrimWhiteSpaceOnRequest(command.Customers[i]);

        return command;
    }
}
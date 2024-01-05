using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Exceptions;
using Core.Common.Helpers;
using MediatR;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Commands;

public class LoadCustomersCommandHandler : CustomerCommandBase,
    IRequestHandler<LoadCustomersCommand, CustomersCommandResponse>
{
    private readonly ICustomerPersistence _customerPersistence;
    private readonly IMapper _mapper;

    public LoadCustomersCommandHandler(ICustomerPersistence customerPersistence,
        IMapper mapper) : base(mapper)
    {
        _customerPersistence = customerPersistence;
        _mapper = mapper;
    }

    public async Task<CustomersCommandResponse> Handle(LoadCustomersCommand request,
        CancellationToken cancellationToken)
    {
        var sanitizedRequest = Sanitize(request);
        var (response, customers) =
            await ValidateCustomersAsync(sanitizedRequest, cancellationToken);

        if (response.ValidationErrors.Count > 0)
            throw new ValidationException(response.ValidationErrors);

        var result = await _customerPersistence.AddManyAsync(customers);

        if (result.Status != RepositoryActionStatus.Created)
        {
            response.Success = false;
            response.Data = null;

            return response;
        }

        response.Data = _mapper.Map<CustomerVm[]>(result.Entity);

        return response;
    }

    protected override void DisposeCore()
    {
        _customerPersistence?.Dispose();
    }
}
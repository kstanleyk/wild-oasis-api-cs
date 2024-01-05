using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Exceptions;
using Core.Common.Helpers;
using MediatR;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Commands;

public class CreateCustomerCommandHandler : CustomerCommandBase, IRequestHandler<CreateCustomerCommand, CustomerCommandResponse>
{
    private readonly ICustomerPersistence _customerPersistence;
    private readonly IMapper _mapper;

    public CreateCustomerCommandHandler(ICustomerPersistence customerPersistence, IMapper mapper) : base(mapper)
    {
        _customerPersistence = customerPersistence;
        _mapper = mapper;
    }

    public async Task<CustomerCommandResponse> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var sanitizedRequest = Sanitize(request);
        var (response, branch) = await ValidateCustomerAsync(sanitizedRequest, cancellationToken);

        if (response.ValidationErrors.Count > 0)
            throw new ValidationException(response.ValidationErrors);

        var result = await _customerPersistence.AddAsync(branch);

        if (result.Status != RepositoryActionStatus.Created)
        {
            response.Success = false;
            response.Data = null;

            return response;
        }

        response.Data = _mapper.Map<CustomerVm>(result.Entity);

        return response;
    }

    protected override void DisposeCore()
    {
        _customerPersistence?.Dispose();
    }
}
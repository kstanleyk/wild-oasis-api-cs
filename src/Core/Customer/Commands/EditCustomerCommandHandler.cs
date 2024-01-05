using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Exceptions;
using Core.Common.Helpers;
using MediatR;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Customer.Commands;

public class EditCustomerCommandHandler : CustomerCommandBase, IRequestHandler<EditCustomerCommand, CustomerCommandResponse>
{
    private readonly ICustomerPersistence _customerPersistence;
    private readonly IMapper _mapper;

    public EditCustomerCommandHandler(ICustomerPersistence customerPersistence, IMapper mapper) : base(mapper)
    {
        _customerPersistence = customerPersistence;
        _mapper = mapper;
    }

    public async Task<CustomerCommandResponse> Handle(EditCustomerCommand request, CancellationToken cancellationToken)
    {
        var sanitizedRequest = Sanitize(request);
        var (response, branch) = await ValidateCustomerAsync(sanitizedRequest, cancellationToken);

        if (response.ValidationErrors.Count > 0)
            throw new ValidationException(response.ValidationErrors);

        var result = await _customerPersistence.EditAsync(branch);

        if (result.Status != RepositoryActionStatus.Updated)
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
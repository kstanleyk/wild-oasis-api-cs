using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Exceptions;
using Core.Common.Helpers;
using MediatR;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Vm;

namespace WildOasis.Application.Cabin.Commands;

public class LoadCabinsCommandHandler : CabinCommandBase,
    IRequestHandler<LoadCabinsCommand, CabinsCommandResponse>
{
    private readonly ICabinPersistence _cabinPersistence;
    private readonly IMapper _mapper;

    public LoadCabinsCommandHandler(ICabinPersistence cabinPersistence,
        IMapper mapper) : base(mapper)
    {
        _cabinPersistence = cabinPersistence;
        _mapper = mapper;
    }

    public async Task<CabinsCommandResponse> Handle(LoadCabinsCommand request,
        CancellationToken cancellationToken)
    {
        var sanitizedRequest = Sanitize(request);
        var (response, cabins) =
            await ValidateCabinsAsync(sanitizedRequest, cancellationToken);

        if (response.ValidationErrors.Count > 0)
            throw new ValidationException(response.ValidationErrors);

        var result = await _cabinPersistence.AddManyAsync(cabins);

        if (result.Status != RepositoryActionStatus.Created)
        {
            response.Success = false;
            response.Data = null;

            return response;
        }

        response.Data = _mapper.Map<CabinVm[]>(result.Entity);

        return response;
    }

    protected override void DisposeCore()
    {
        _cabinPersistence?.Dispose();
    }
}
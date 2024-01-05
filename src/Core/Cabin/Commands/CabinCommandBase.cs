using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common.Core;

namespace WildOasis.Application.Cabin.Commands;

public abstract class CabinCommandBase : RequestHandlerBase
{
    private readonly IMapper _mapper;

    protected CabinCommandBase(IMapper mapper)
    {
        _mapper = mapper;
    }

    protected async
        Task<(CabinCommandResponse response, Domain.Entity.Cabin
            branch)> ValidateCabinAsync(
        CabinCommand request,
        CancellationToken cancellationToken)
    {
        var response = new CabinCommandResponse();

        var validator = new CabinCommandValidator();
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

        var entity = _mapper.Map<Domain.Entity.Cabin>(request.Cabin);

        return (response, entity);
    }


    protected async Task<(CabinsCommandResponse response, Domain.Entity.Cabin[] branches)>
        ValidateCabinsAsync(CabinsCommand request, CancellationToken cancellationToken)
    {
        var response = new CabinsCommandResponse();

        if (request.Cabins is null)
        {
            response.Success = false;
            response.ValidationErrors.Add("list of Cabins can not be empty");
            return (response, null);
        }

        var validator = new CabinsCommandValidator();
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

        var branches = _mapper.Map<Domain.Entity.Cabin[]>(request.Cabins);

        return (response, branches);
    }
    protected static CabinCommand Sanitize(CabinCommand command)
    {
        if (command.Cabin != null)
            command.Cabin = PropertySanitizer.TrimWhiteSpaceOnRequest(command.Cabin);

        return command;
    }

    protected static CabinsCommand Sanitize(CabinsCommand command)
    {
        if (command.Cabins == null) return command;
        for (var i = 0; i <= command.Cabins.Length - 1; i++)
            command.Cabins[i] = PropertySanitizer.TrimWhiteSpaceOnRequest(command.Cabins[i]);

        return command;
    }
}
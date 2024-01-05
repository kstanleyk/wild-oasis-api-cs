using System;
using System.Collections.Generic;
using Core.Common.Helpers;
using Moq;
using WildOasis.Domain.Contracts.Persistence;

namespace WildOasis.Application.Test.Cabin;

public static class CabinPersistenceMock
{
    public static Mock<ICabinPersistence> GetCabinPersistence()
    {
        var createdDate = DateTime.Now;

        var branches = new List<Domain.Entity.Cabin>
        {
            new()
            {
                Description = new string('d', 75),
            },
            new()
            {
                Description = new string('d', 75),
            }
        };

        var persistence = new Mock<ICabinPersistence>();
        persistence.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(branches.ToArray);

        persistence.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(branches.Count);

        persistence.Setup(repo => repo.AddAsync(It.IsAny<Domain.Entity.Cabin>()))
            .ReturnsAsync((Domain.Entity.Cabin entity) =>
            {
                branches.Add(entity);
                return new RepositoryActionResult<Domain.Entity.Cabin>(entity,
                    RepositoryActionStatus.Created);
            });

        persistence.Setup(repo => repo.EditAsync(It.IsAny<Domain.Entity.Cabin>()))
            .ReturnsAsync((Domain.Entity.Cabin entity) =>
            {
                return new RepositoryActionResult<Domain.Entity.Cabin>(entity,
                    RepositoryActionStatus.Updated);
            });

        return persistence;
    }
}
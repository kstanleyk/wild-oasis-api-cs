using System;
using System.Collections.Generic;
using Core.Common.Helpers;
using Moq;
using WildOasis.Domain.Contracts.Persistence.Common;

namespace WildOasis.Application.Test.Common.Branch;

public static class BranchPersistenceMock
{
    public static Mock<IBranchPersistence> GetBranchPersistence()
    {
        var createdDate = DateTime.Now;

        var branches = new List<WildOasis.Domain.Entity.Common.Branch>
        {
            new()
            {
                Tenant = new string('t', 10),
                Code = new string('c', 5),
                Description = new string('d', 75),
                BranchName = new string('b', 75),
                BranchShortName = new string('b', 35),
                StationCode = new string('s', 10),
                Address = new string('a', 50),
                Telephone = new string('t', 35),
                Region = new string('r', 2),
                Motto = new string('m', 150),
                HeadOffice = new string('h', 2),
                Employer = new string('e', 5),
                BranchType = new string('b', 2),
                BranchTown = new string('b', 35),
            },
            new()
            {
                Tenant = new string('t', 10),
                Code = new string('c', 5),
                Description = new string('d', 75),
                BranchName = new string('b', 75),
                BranchShortName = new string('b', 35),
                StationCode = new string('s', 10),
                Address = new string('a', 50),
                Telephone = new string('t', 35),
                Region = new string('r', 2),
                Motto = new string('m', 150),
                HeadOffice = new string('h', 2),
                Employer = new string('e', 5),
                BranchType = new string('b', 2),
                BranchTown = new string('b', 35),
            }
        };

        var persistence = new Mock<IBranchPersistence>();
        persistence.Setup(repo => repo.GetAllAsync())
            .ReturnsAsync(branches.ToArray);

        persistence.Setup(repo => repo.GetCountAsync())
            .ReturnsAsync(branches.Count);

        persistence.Setup(repo => repo.AddAsync(It.IsAny<WildOasis.Domain.Entity.Common.Branch>()))
            .ReturnsAsync((WildOasis.Domain.Entity.Common.Branch entity) =>
            {
                branches.Add(entity);
                return new RepositoryActionResult<WildOasis.Domain.Entity.Common.Branch>(entity,
                    RepositoryActionStatus.Created);
            });

        persistence.Setup(repo => repo.EditAsync(It.IsAny<WildOasis.Domain.Entity.Common.Branch>()))
            .ReturnsAsync((WildOasis.Domain.Entity.Common.Branch entity) =>
            {
                return new RepositoryActionResult<WildOasis.Domain.Entity.Common.Branch>(entity,
                    RepositoryActionStatus.Updated);
            });

        return persistence;
    }
}
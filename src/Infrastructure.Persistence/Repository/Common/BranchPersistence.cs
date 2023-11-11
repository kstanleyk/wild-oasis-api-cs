using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Helpers;
using WildOasis.Domain.Contracts.Persistence.Common;
using WildOasis.Domain.Entity.Common;

namespace WildOasis.Infrastructure.Persistence.Repository.Common
{
    public class BranchPersistence : DataPersistenceBase<Branch>, IBranchPersistence
    {
        public BranchPersistence(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public override async Task<RepositoryActionResult<Branch>> AddAsync(Branch entity)
        {
            await using var tx = await Context.Database.BeginTransactionAsync();
            try
            {
                var lastBranch = DbSet.OrderByDescending(x => x.Code).ToArray().FirstOrDefault();
                var serial = lastBranch == null
                    ? "1".ToTwoChar()
                    : (lastBranch.Code.ToNumValue() + 1)
                    .ToNumValue().ToString(CultureInfo.InvariantCulture).ToTwoChar();
                entity.Code = serial;

                DbSet.Add(entity);
                var result = await SaveChangesAsync();
                if (result == 0)
                {
                    return new RepositoryActionResult<Branch>(entity, RepositoryActionStatus.NothingModified);
                }

                await tx.CommitAsync();
                return new RepositoryActionResult<Branch>(entity, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new RepositoryActionResult<Branch>(null, RepositoryActionStatus.Error, ex);
            }
        }

        protected override async Task<Branch> ItemToGetAsync(string tenant, string branch) =>
            await GetFirstOrDefaultAsync(x => x.Code == branch);

        protected override async Task<Branch> ItemToGetAsync(Branch branch) =>
            await GetFirstOrDefaultAsync(x => x.Code == branch.Code);
    }
}
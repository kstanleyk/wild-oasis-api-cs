using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Helpers;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Entity;

namespace WildOasis.Infrastructure.Persistence.Repository
{
    public class CabinPersistence : DataPersistenceBase<Cabin>, ICabinPersistence
    {
        public CabinPersistence(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public override async Task<RepositoryActionResult<Cabin>> AddAsync(Cabin cabin)
        {
            await using var tx = await Context.Database.BeginTransactionAsync();
            try
            {
                var lastBranch = DbSet.OrderByDescending(x => x.Name).ToArray().FirstOrDefault();
                var serial = lastBranch == null
                    ? "1".ToTwoChar()
                    : (lastBranch.Name.ToNumValue() + 1)
                    .ToNumValue().ToString(CultureInfo.InvariantCulture).ToTwoChar();
                cabin.Name = serial;

                DbSet.Add(cabin);
                var result = await SaveChangesAsync();
                if (result == 0)
                {
                    return new RepositoryActionResult<Cabin>(cabin, RepositoryActionStatus.NothingModified);
                }

                await tx.CommitAsync();
                return new RepositoryActionResult<Cabin>(cabin, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new RepositoryActionResult<Cabin>(null, RepositoryActionStatus.Error, ex);
            }
        }

        protected override async Task<Cabin> ItemToGetAsync(string cabin) =>
            await GetFirstOrDefaultAsync(x => x.Name == cabin);

        protected override async Task<Cabin> ItemToGetAsync(Cabin branch) =>
            await GetFirstOrDefaultAsync(x => x.Name == branch.Name);
    }
}
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Helpers;
using WildOasis.Domain.Contracts.Persistence;
using WildOasis.Domain.Entity;

namespace WildOasis.Infrastructure.Persistence.Repository
{
    public class CustomerPersistence : DataPersistenceBase<Customer>, ICustomerPersistence
    {
        public CustomerPersistence(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public override async Task<RepositoryActionResult<Customer>> AddAsync(Customer customer)
        {
            await using var tx = await Context.Database.BeginTransactionAsync();
            try
            {
                var lastBranch = DbSet.OrderByDescending(x => x.Id).ToArray().FirstOrDefault();
                var serial = lastBranch == null
                    ? "1".ToTwoChar()
                    : (lastBranch.Id.ToNumValue() + 1)
                    .ToNumValue().ToString(CultureInfo.InvariantCulture).ToFiveChar();
                customer.Id = serial;

                DbSet.Add(customer);
                var result = await SaveChangesAsync();
                if (result == 0)
                {
                    return new RepositoryActionResult<Customer>(customer, RepositoryActionStatus.NothingModified);
                }

                await tx.CommitAsync();
                return new RepositoryActionResult<Customer>(customer, RepositoryActionStatus.Created);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                return new RepositoryActionResult<Customer>(null, RepositoryActionStatus.Error, ex);
            }
        }

        protected override async Task<Customer> ItemToGetAsync(string customer) =>
            await GetFirstOrDefaultAsync(x => x.Id == customer);

        protected override async Task<Customer> ItemToGetAsync(Customer branch) =>
            await GetFirstOrDefaultAsync(x => x.Id == branch.Id);
    }
}
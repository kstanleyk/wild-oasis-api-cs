using System;
using VishiHolding.Domain.Entity.Inventory;
using VishiHolding.Infrastructure.Persistence;
using WildOasis.Infrastructure.Persistence;

namespace VishiHolding.Integration.Test.Base
{
    public class Utilities
    {
        public static void InitializeDbForTests(WildOasisContext context)
        {
            context.CategorySet.Add(new Category
            {
                Tenant = new string('1', 3),
                Code = new string('1', 3),
                Description = new string('a', 50),
                CreatedOn = DateTime.Now
            });
            context.CategorySet.Add(new Category
            {
                Tenant = new string('1', 3),
                Code = new string('2', 3),
                Description = new string('b', 50),
                CreatedOn = DateTime.Now
            });

            context.SaveChanges();
        }
    }
}

using GreenDonut;
using HotChocolate.DataLoader;
using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.DataLoader
{
    public class BookByIdDataLoader : BatchDataLoader<int, Book>
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public BookByIdDataLoader(IBatchScheduler batchScheduler, IDbContextFactory<AppDbContext> dbContextFactory) : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Book>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using AppDbContext dbContext = _dbContextFactory.CreateDbContext();
            return await dbContext.Books.Where(b => keys.Contains(b.Id)).ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}

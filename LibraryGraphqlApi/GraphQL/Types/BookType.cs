using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.GraphQL.DataLoader;
using LibraryGraphqlApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.Types
{
    public class BookType : ObjectType<Book>
    {
        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<BookByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Authors)
                .ResolveWith<BookResolvers>(t => t.GetAuthorsAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .UsePaging<NonNullType<AuthorType>>()
                .UseFiltering()
                .UseSorting()
                .Name("authors");
        }

        private class BookResolvers
        {
            public async Task<IEnumerable<Author>> GetAuthorsAsync(Book book, [ScopedService] AppDbContext dbContext, AuthorByIdDataLoader authorById, CancellationToken cancellationToken)
            {
                int[] authorIds = await dbContext.Books
                    .Where(b => b.Id == book.Id)
                    .Include(b => b.Authors)
                    .SelectMany(b => b.Authors.Select(a => a.Id))
                    .ToArrayAsync();

                return await authorById.LoadAsync(authorIds, cancellationToken);
            }
        }
    }
}

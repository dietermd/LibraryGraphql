using HotChocolate;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.GraphQL.DataLoader;
using LibraryGraphqlApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL.Types
{
    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            //descriptor.Field(t => t.DOB).Ignore();

            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<AuthorByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Books)
                .ResolveWith<AuthorResolvers>(t => t.GetBooksAsync(default, default, default, default))
                .UseDbContext<AppDbContext>()
                .UsePaging<NonNullType<BookType>>()
                .UseFiltering()
                .UseSorting()
                .Name("books");
        }

        private class AuthorResolvers
        {
            public async Task<IEnumerable<Book>> GetBooksAsync(Author author, [ScopedService] AppDbContext dbContext, BookByIdDataLoader bookById, CancellationToken cancellationToken)
            {
                int[] booksIds = await dbContext.Authors
                    .Where(a => a.Id == author.Id)
                    .Include(a => a.Books)
                    .SelectMany(a => a.Books.Select(a => a.Id))
                    .ToArrayAsync();

                return await bookById.LoadAsync(booksIds, cancellationToken);
            }
        }
    }
}

using HotChocolate;
using HotChocolate.Types.Relay;
using LibraryGraphqlApi.Data;
using LibraryGraphqlApi.GraphQL.DataLoader;
using LibraryGraphqlApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryGraphqlApi.GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        public Task<List<Book>> GetBooks([ScopedService] AppDbContext context) => context.Books.ToListAsync();

        public Task<Book> GetBookAsync([ID(nameof(Book))] int id, BookByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);

        [UseApplicationDbContext]
        public Task<List<Author>> GetAuthors([ScopedService] AppDbContext context) => context.Authors.ToListAsync();

        public Task<Author> GetAuthorAsync([ID(nameof(Author))] int id, AuthorByIdDataLoader dataLoader, CancellationToken cancellationToken) => dataLoader.LoadAsync(id, cancellationToken);
    }
}
